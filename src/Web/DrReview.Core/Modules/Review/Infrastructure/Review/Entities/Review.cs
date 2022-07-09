﻿namespace DrReview.Modules.Review.Infrastructure.Review.Entities
{
    using System;
    using CSharpVitamins;
    using DrReview.Common.Infrastructure.Entities;

    public partial class Review : AggregateRoot
    {
        public Review(
            long id,
            Guid uid,
            DateTime? deletedOn,
            DateTime modifiedOn,
            long reviewerFK,
            long revieweeFK,
            string? comment,
            decimal score)
            : base(id, uid, new ShortGuid(uid), deletedOn, modifiedOn)
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

        public string? Comment { get; private set; }

        public decimal Score { get; private set; }
    }
}
