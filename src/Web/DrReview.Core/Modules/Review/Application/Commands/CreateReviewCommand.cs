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

    public class CreateReviewCommand : ICommand<Result<EmptyValue>>
    {
        public CreateReviewCommand(string revieweeSuid, string? comment, decimal score, bool anonymous)
        {
            RevieweeSuid = revieweeSuid;
            Comment = comment;
            Score = score;
            Anonymous = anonymous;
        }

        public string RevieweeSuid { get; init; }

        public string? Comment { get; init; }

        public decimal Score { get; init; }

        public bool Anonymous { get; init; }
    }

    public class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand, Result<EmptyValue>>
    {
        private readonly ICurrentUser _currentUser;

        private readonly IReviewUnitOfWork _unitOfWork;

        private readonly ReviewReadOnlyDatabaseContext _readonlyContext;

        public CreateReviewCommandHandler(ICurrentUser currentUser, IReviewUnitOfWork unitOfWork, ReviewReadOnlyDatabaseContext readonlyContext)
        {
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _readonlyContext = readonlyContext;
        }

        public async Task<Result<EmptyValue>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            Reviewer? reviewer = await _readonlyContext.Reviewers.FirstOrDefaultAsync(r => r.Uid == _currentUser.Uid);

            if (reviewer is null)
            {
                return Result.NotFound<EmptyValue>(ResultCodes.UserNotFound);
            }

            Reviewee? reviewee = await _readonlyContext.Reviewees.FirstOrDefaultAsync(r => r.Suid == request.RevieweeSuid);

            if (reviewee is null)
            {
                return Result.NotFound<EmptyValue>(ResultCodes.DoctorNotFound);
            }

            bool reviewExists = await _readonlyContext.Reviews.AnyAsync(r => r.RevieweeFK == reviewee.Id && r.ReviewerFK == reviewer.Id && r.DeletedOn == null);

            if (reviewExists)
            {
                return Result.Invalid<EmptyValue>(ResultCodes.OneReviewPerReviewee);
            }

            Result<Review> newReview = Review.Create(
                                                     reviewerFK: reviewer.Id,
                                                     revieweeFK: reviewee.Id,
                                                     comment: request.Comment,
                                                     score: request.Score,
                                                     anonymous: request.Anonymous);

            if (newReview.IsFailure)
            {
                return Result.FromError<EmptyValue>(newReview);
            }

            _unitOfWork.Reviews.InsertReview(newReview);

            await _unitOfWork.SaveAsync();

            return Result.Ok<EmptyValue>(EmptyValue.Value);
        }
    }
}
