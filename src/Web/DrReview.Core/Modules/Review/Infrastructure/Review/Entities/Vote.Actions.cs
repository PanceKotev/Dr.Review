namespace DrReview.Modules.Review.Infrastructure.Review.Entities
{
    using DrReview.Common.Results;

    public partial class Vote
    {
        public Result Update(bool? upvoted)
        {
            Upvoted = upvoted;

            return Result.Ok();
        }
    }
}
