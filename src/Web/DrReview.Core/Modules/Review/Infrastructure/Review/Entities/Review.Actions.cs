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
    }
}
