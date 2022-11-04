namespace DrReview.Modules.Review.Infrastructure.Review.Entities
{
    using System;
    using DrReview.Common.Results;

    public partial class Review
    {
        public Result<Review> Delete()
        {
            this.DeletedOn = DateTime.UtcNow;

            return Result.Ok(this);
        }

        public Result<Review> Update(
                                     string? comment,
                                     decimal score,
                                     bool anonymous)
        {
            if (score < 0 || score > 5 || ((double)score % 0.5 != 0))
            {
                return Result.Invalid<Review>(ResultCodes.InvalidReviewScore);
            }

            string? trimmedComment = string.IsNullOrWhiteSpace(comment) ? null : comment.Trim();

            this.Comment = trimmedComment;
            this.Score = score;
            this.Anonymous = anonymous;
            this.ModifiedOn = DateTime.UtcNow;

            return Result.Ok(this);
        }
    }
}
