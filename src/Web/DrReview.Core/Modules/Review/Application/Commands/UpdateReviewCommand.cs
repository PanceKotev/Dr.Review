namespace DrReview.Modules.Review.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Modules.Review.Infrastructure.Common.Contexts;
    using DrReview.Modules.Review.Infrastructure.Common.UnitOfWork.Interfaces;
    using DrReview.Modules.Review.Infrastructure.Review.Entities;
    using Microsoft.EntityFrameworkCore;

    public class UpdateReviewCommand : ICommand<Result<EmptyValue>>
    {
        public UpdateReviewCommand(string? comment, decimal score, string reviewSuid)
        {
            Comment = comment;
            Score = score;
            ReviewSuid = reviewSuid;
        }

        public string ReviewSuid { get; init; }

        public string? Comment { get; init; }

        public decimal Score { get; init; }
    }

    public class UpdateReviewCommandHandler : ICommandHandler<UpdateReviewCommand, Result<EmptyValue>>
    {
        private readonly IReviewUnitOfWork _unitOfWork;

        private readonly ReviewReadOnlyDatabaseContext _readonlyContext;

        public UpdateReviewCommandHandler(IReviewUnitOfWork unitOfWork, ReviewReadOnlyDatabaseContext readonlyContext)
        {
            _unitOfWork = unitOfWork;
            _readonlyContext = readonlyContext;
        }

        public async Task<Result<EmptyValue>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            Review? review = await _readonlyContext.Reviews.FirstOrDefaultAsync(r => r.Suid == request.ReviewSuid);

            if (review is null)
            {
                return Result.NotFound<EmptyValue>(ResultCodes.ReviewNotFound);
            }

            review.Update(
                          comment: request.Comment,
                          score: request.Score);

            _unitOfWork.Reviews.UpdateReview(review);

            await _unitOfWork.SaveAsync();

            return Result.Ok<EmptyValue>(EmptyValue.Value);
        }
    }
}
