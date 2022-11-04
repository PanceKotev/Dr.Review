namespace DrReview.Modules.Review.Infrastructure.Review.Entities
{
    using System;
    using CSharpVitamins;
    using DrReview.Common.Infrastructure.Entities;

    public partial class Review : AggregateRoot
    {
        private Review(
            long id,
            Guid uid,
            DateTime? deletedOn,
            DateTime modifiedOn,
            long reviewerFK,
            long revieweeFK,
            string? comment,
            decimal score,
            long upvotes,
            long downvotes,
            bool anonymous)
            : base(id, uid, new ShortGuid(uid), deletedOn, modifiedOn)
        {
            ReviewerFK = reviewerFK;
            RevieweeFK = revieweeFK;
            Comment = comment;
            Score = score;
            Upvotes = upvotes;
            Downvotes = downvotes;
            Anonymous = anonymous;
        }

        public long ReviewerFK { get; init; }

        public virtual Reviewer? Reviewer { get; init; }

        public long RevieweeFK { get; init; }

        public virtual Reviewee? Reviewee { get; init; }

        public string? Comment { get; private set; }

        public decimal Score { get; private set; }

        public long Upvotes { get; init; }

        public long Downvotes { get; init; }

        public bool Anonymous { get; private set; }
    }
}
