namespace DrReview.Modules.Review.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Modules.Review.Infrastructure.Common.Contexts;
    using DrReview.Modules.Review.Infrastructure.Common.UnitOfWork.Interfaces;
    using DrReview.Modules.Review.Infrastructure.Review.Entities;
    using Microsoft.EntityFrameworkCore;

    public class DeleteReviewCommand : ICommand<Result<EmptyValue>>
    {
        public DeleteReviewCommand(string reviewSuid)
        {
            ReviewSuid = reviewSuid;
        }

        public string ReviewSuid { get; init; }
    }

    public class DeleteReviewCommandHandler : ICommandHandler<DeleteReviewCommand, Result<EmptyValue>>
    {
        private readonly IReviewUnitOfWork _unitOfWork;

        private readonly ReviewReadOnlyDatabaseContext _readonlyContext;

        public DeleteReviewCommandHandler(IReviewUnitOfWork unitOfWork, ReviewReadOnlyDatabaseContext readonlyContext)
        {
            _unitOfWork = unitOfWork;
            _readonlyContext = readonlyContext;
        }

        public async Task<Result<EmptyValue>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            Review? review = await _readonlyContext.Reviews.FirstOrDefaultAsync(r => r.Suid == request.ReviewSuid);

            if (review is null)
            {
                return Result.NotFound<EmptyValue>(ResultCodes.ReviewNotFound);
            }

            review.Delete();

            _unitOfWork.Reviews.UpdateReview(review);

            await _unitOfWork.SaveAsync();

            return Result.Ok<EmptyValue>(EmptyValue.Value);
        }
    }
}
