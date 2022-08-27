namespace DrReview.Modules.Review.Infrastructure.Review.Entities
{
    using System;
    using CSharpVitamins;
    using DrReview.Common.Infrastructure.Entities;

    public partial class Vote : BaseEntity
    {
        private Vote(long id, Guid uid, DateTime? deletedOn, DateTime modifiedOn, bool? upvoted, long reviewerFK, long reviewFK)
            : base(id, uid, new ShortGuid(uid), deletedOn, modifiedOn)
        {
            Upvoted = upvoted;
            ReviewerFK = reviewerFK;
            ReviewFK = reviewFK;
        }

        public bool? Upvoted { get; set; }

        public long ReviewerFK { get; init; }

        public virtual Reviewer? Reviewer { get; set; }

        public virtual Review? Review { get; set; }

        public long ReviewFK { get; init; }
    }
}
