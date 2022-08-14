namespace DrReview.Modules.Review.Infrastructure.Review.Entities
{
    using System;
    using DrReview.Common.Extensions;
    using DrReview.Common.Results;

    public partial class Vote
    {
        public static Result<Vote> Create(
                                         bool upvote,
                                         long reviewerFK,
                                         long reviewFK)
        {
            if (!reviewerFK.IsValidId() || !reviewFK.IsValidId())
            {
                return Result.Invalid<Vote>("Invalid reivewer or review");
            }

            return Result.Ok(new Vote(
                   id: default,
                   uid: Guid.NewGuid(),
                   deletedOn: null,
                   modifiedOn: DateTime.UtcNow,
                   upvoted: upvote,
                   reviewerFK: reviewerFK,
                   reviewFK: reviewFK));
        }
    }
}
