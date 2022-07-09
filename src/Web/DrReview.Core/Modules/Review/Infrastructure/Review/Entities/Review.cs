namespace DrReview.Modules.Review.Infrastructure.Review.Entities
{
    using System;
    using DrReview.Common.Infrastructure.Entities;

    public class Review : AggregateRoot
    {
        public Review(
            long id,
            Guid uid,
            string suid,
            DateTime? deletedOn,
            DateTime modifiedOn,
            long reviewerFK,
            long revieweeFK,
            string? comment,
            decimal score)
            : base(id, uid, suid, deletedOn, modifiedOn)
        {
            ReviewerFK = reviewerFK;
            RevieweeFK = revieweeFK;
            Comment = comment;
            Score = score;
        }

        public long ReviewerFK { get; init; }

        public virtual Reviewer? Reviewer { get; init; }

        public long RevieweeFK { get; init; }

        public virtual Reviewee? Reviewee { get; init; }

        public string? Comment { get; init; }

        public decimal Score { get; init; }
    }
}
