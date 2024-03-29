﻿namespace DrReview.Modules.Review.Application.Commands
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
        public UpdateReviewCommand(string? comment, decimal score, string reviewSuid, bool anonymous)
        {
            Comment = comment;
            Score = score;
            ReviewSuid = reviewSuid;
            Anonymous = anonymous;
        }

        public string ReviewSuid { get; init; }

        public string? Comment { get; init; }

        public decimal Score { get; init; }

        public bool Anonymous { get; init; }
    }

    public class UpdateReviewCommandHandler : ICommandHandler<UpdateReviewCommand, Result<EmptyValue>>
    {
        private readonly IReviewUnitOfWork _unitOfWork;

        private readonly ICurrentUser _currentUser;

        private readonly ReviewReadOnlyDatabaseContext _readonlyContext;

        public UpdateReviewCommandHandler(
            IReviewUnitOfWork unitOfWork,
            ReviewReadOnlyDatabaseContext readonlyContext,
            ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _readonlyContext = readonlyContext;
        }

        public async Task<Result<EmptyValue>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            Review? review = await _readonlyContext.Reviews.Include(r => r.Reviewer).FirstOrDefaultAsync(r => r.Suid == request.ReviewSuid);

            if (review is null)
            {
                return Result.NotFound<EmptyValue>(ResultCodes.ReviewNotFound);
            }

            if (review.Reviewer!.Uid != _currentUser.Uid)
            {
                return Result.Invalid<EmptyValue>(ResultCodes.NoPermission);
            }

            review.Update(
                        comment: request.Comment,
                        score: request.Score,
                        anonymous: request.Anonymous);

            _unitOfWork.Reviews.UpdateReview(review);

            await _unitOfWork.SaveAsync();

            return Result.Ok<EmptyValue>(EmptyValue.Value);
        }
    }
}
