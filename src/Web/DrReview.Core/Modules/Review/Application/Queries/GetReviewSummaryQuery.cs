namespace DrReview.Modules.Review.Application.Queries
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Contracts.Dtos;
    using DrReview.Contracts.Enums;
    using DrReview.Modules.Review.Infrastructure.Common.Contexts;
    using DrReview.Modules.Review.Infrastructure.Review.Entities;
    using Microsoft.EntityFrameworkCore;

    public class GetReviewSummaryQuery : IQuery<Result<GetReviewSummaryDto>>
    {
        public GetReviewSummaryQuery(string revieweeSuid)
        {
            RevieweeSuid = revieweeSuid;
        }

        public string RevieweeSuid { get; init; }
    }

    public class GetReviewSummaryQueryHandler : IQueryHandler<GetReviewSummaryQuery, Result<GetReviewSummaryDto>>
    {
        private readonly ReviewReadOnlyDatabaseContext _readonlyDatabaseContext;

        public GetReviewSummaryQueryHandler(ReviewReadOnlyDatabaseContext readOnlyDatabaseContext)
        {
            _readonlyDatabaseContext = readOnlyDatabaseContext;
        }

        public async Task<Result<GetReviewSummaryDto>> Handle(GetReviewSummaryQuery request, CancellationToken cancellationToken)
        {
            Reviewee? reviewee = await _readonlyDatabaseContext.Reviewees.FirstOrDefaultAsync(r => r.Suid == request.RevieweeSuid);

            if (reviewee is null)
            {
                return Result.Invalid<GetReviewSummaryDto>(ResultCodes.DoctorNotFound);
            }

            List<decimal> allReviewsForReviewee = await _readonlyDatabaseContext.Reviews
                                                                                       .Where(r => r.RevieweeFK == reviewee.Id)
                                                                                       .Select(r => r.Score)
                                                                                       .ToListAsync();

            Dictionary<int, int> dictionaryByStars = new Dictionary<int, int>
            {
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 0 }
            };

            decimal sumScores = 0;

            foreach (decimal reviewScore in allReviewsForReviewee)
            {
                bool validReviewValue = dictionaryByStars.ContainsKey((int)reviewScore);

                if (!validReviewValue)
                {
                    continue;
                }

                int calculatedValue = dictionaryByStars.GetValueOrDefault((int)reviewScore) + 1;

                dictionaryByStars[(int)reviewScore] = calculatedValue;

                sumScores += reviewScore;
            }

            decimal averageScore = allReviewsForReviewee.Count < 1 ? 0 : sumScores / allReviewsForReviewee.Count;

            return Result.Ok(new GetReviewSummaryDto(averageScore, dictionaryByStars));
        }
    }
}
