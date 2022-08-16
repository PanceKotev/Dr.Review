namespace DrReview.Modules.Review.Application.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Contracts.Dtos;
    using DrReview.Contracts.Filters;
    using DrReview.Modules.Review.Infrastructure.Common.Contexts;
    using DrReview.Modules.Review.Infrastructure.Review.Entities;
    using LinqKit;
    using Microsoft.EntityFrameworkCore;

    public class GetReviewsQuery : IQuery<Result<GetReviewsDto>>
    {
        public GetReviewsQuery(int startPage, int itemsPerPage, string? revieweeSuid, string? reviewerSuid)
        {
            Filter = new GetReviewsFilter(
                                          pageStart: startPage,
                                          itemsPerPage: itemsPerPage,
                                          revieweeSuid: revieweeSuid,
                                          reviewerSuid: reviewerSuid);
        }

        public GetReviewsFilter Filter { get; init; }
    }

    public class GetReviewsQueryHandler : IQueryHandler<GetReviewsQuery, Result<GetReviewsDto>>
    {
        private readonly ReviewReadOnlyDatabaseContext _readonlyDatabaseContext;

        private readonly ICurrentUser _currentUser;

        public GetReviewsQueryHandler(ReviewReadOnlyDatabaseContext readonlyDatabaseContext, ICurrentUser currentUser)
        {
            _readonlyDatabaseContext = readonlyDatabaseContext;
            _currentUser = currentUser;
        }

        public async Task<Result<GetReviewsDto>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            ExpressionStarter<Review>? predicate = PredicateBuilder.New<Review>(true);

            if (!string.IsNullOrEmpty(request.Filter.RevieweeSuid))
            {
                predicate.And(r => r.Reviewee != null && r.Reviewee.Suid == request.Filter.RevieweeSuid);
            }

            int skipNumberOfPages = request.Filter.StartPage * request.Filter.ItemsPerPage;

            List<Review> fromSqlResult = await _readonlyDatabaseContext.Reviews
                                                  .Include(r => r.Reviewer)
                                                  .Include(r => r.Reviewee)
                                                  .Where(predicate)
                                                  .OrderBy(r => r.Reviewer!.Suid == _currentUser.Suid)
                                                  .Skip(skipNumberOfPages)
                                                  .Take(request.Filter.ItemsPerPage)
                                                  .ToListAsync();

            List<GetReviewDto> reviews = new List<GetReviewDto>();

            GetReviewDto? reviewFromCurrentUser = null;

            fromSqlResult.ForEach(r =>
            {
                if (r.Reviewer!.Uid == _currentUser.Uid)
                {
                    reviewFromCurrentUser = new GetReviewDto(
                                                            r.Suid,
                                                            $@"{r.Reviewer!.FirstName} {r.Reviewer!.LastName}",
                                                            $@"{r.Reviewee!.FirstName} {r.Reviewee!.LastName}",
                                                            r.ModifiedOn,
                                                            r.Comment,
                                                            r.Score,
                                                            r.Upvotes,
                                                            r.Downvotes);
                }
                else
                {
                    reviews.Add(new GetReviewDto(
                                                r.Suid,
                                                $@"{r.Reviewer!.FirstName} {r.Reviewer!.LastName}",
                                                $@"{r.Reviewee!.FirstName} {r.Reviewee!.LastName}",
                                                r.ModifiedOn,
                                                r.Comment,
                                                r.Score,
                                                r.Upvotes,
                                                r.Downvotes));
                }
            });

            GetReviewsDto result = new GetReviewsDto(
                                                     reviews: reviews,
                                                     currentUserReview: reviewFromCurrentUser);

            return Result.Ok(result);
        }
    }
}
