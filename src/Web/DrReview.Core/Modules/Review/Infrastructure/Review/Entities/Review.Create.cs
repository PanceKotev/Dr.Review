namespace DrReview.Modules.Review.Infrastructure.Review.Entities
{
    using System;
    using DrReview.Common.Results;

    public partial class Review
    {
        public static Result<Review> Create(
                                            long reviewerFK,
                                            long revieweeFK,
                                            string? comment,
                                            decimal score)
        {
            if (score < 0 || score > 5 || ((double)score % 0.5 != 0))
            {
                return Result.Invalid<Review>(ResultCodes.InvalidReviewScore);
            }

            string? trimmedComment = string.IsNullOrWhiteSpace(comment) ? null : comment.Trim();

            Review newReview = new Review(
                                          id: default,
                                          uid: Guid.NewGuid(),
                                          deletedOn: null,
                                          modifiedOn: DateTime.UtcNow,
                                          reviewerFK: reviewerFK,
                                          revieweeFK: revieweeFK,
                                          comment: trimmedComment,
                                          score: score);

            return Result.Ok(newReview);
        }
    }
}
