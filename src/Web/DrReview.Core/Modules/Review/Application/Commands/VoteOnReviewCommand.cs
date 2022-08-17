namespace DrReview.Modules.Review.Application.Commands
{
    using System.Threading.Tasks;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Modules.Review.Infrastructure.Common.Contexts;
    using DrReview.Modules.Review.Infrastructure.Common.UnitOfWork.Interfaces;
    using DrReview.Modules.Review.Infrastructure.Review.Entities;
    using Microsoft.EntityFrameworkCore;

    public class VoteOnReviewCommand : ICommand<Result<EmptyValue>>
    {
        public VoteOnReviewCommand(string reviewSuid, bool? vote)
        {
            ReviewSuid = reviewSuid;
            Vote = vote;
        }

        public string ReviewSuid { get; init; }

        public bool? Vote { get; init; }
    }

    public class VoteOnReviewCommandHandler : ICommandHandler<VoteOnReviewCommand, Result<EmptyValue>>
    {
        private readonly IReviewUnitOfWork _unitOfWork;

        private readonly ReviewReadOnlyDatabaseContext _readonlyContext;

        private readonly ICurrentUser _currentUser;

        public VoteOnReviewCommandHandler(
            IReviewUnitOfWork unitOfWork,
            ReviewReadOnlyDatabaseContext readonlyContext,
            ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _readonlyContext = readonlyContext;
            _currentUser = currentUser;
        }

        public async Task<Result<EmptyValue>> Handle(VoteOnReviewCommand request, CancellationToken cancellationToken)
        {
            Reviewer? reviewer = await _readonlyContext.Reviewers.FirstOrDefaultAsync(r => r.Uid == _currentUser.Uid);

            if (reviewer is null)
            {
                return Result.NotFound<EmptyValue>(ResultCodes.UserNotFound);
            }

            Review? review = await _readonlyContext.Reviews.FirstOrDefaultAsync(r => r.Suid == request.ReviewSuid);

            if (review is null)
            {
                return Result.NotFound<EmptyValue>(ResultCodes.ReviewNotFound);
            }

            Vote? vote = await _readonlyContext.Votes.FirstOrDefaultAsync(v => v.ReviewFK == review.Id && v.Reviewer != null && v.Reviewer.Uid == _currentUser.Uid);

            if (vote is null)
            {
                vote = Vote.Create(
                                  upvote: request.Vote,
                                  reviewerFK: reviewer.Id,
                                  reviewFK: review.Id);
            }
            else
            {
                vote.Update(request.Vote);
            }

            _unitOfWork.Reviews.UpdateVote(vote);

            await _unitOfWork.SaveAsync();

            return Result.Ok<EmptyValue>(EmptyValue.Value);
        }
    }
}
