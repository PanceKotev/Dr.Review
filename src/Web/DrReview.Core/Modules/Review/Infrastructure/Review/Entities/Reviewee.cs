namespace DrReview.Modules.Review.Infrastructure.Review.Entities
{
    using System;
    using CSharpVitamins;
    using DrReview.Common.Infrastructure.Entities;

    public class Reviewee : BaseEntity
    {
        public Reviewee(long id, Guid uid, DateTime? deletedOn, DateTime modifiedOn, string firstName, string lastName, decimal reviewScore)
            : base(id, uid, new ShortGuid(uid), deletedOn, modifiedOn)
        {
            FirstName = firstName;
            LastName = lastName;
            ReviewScore = reviewScore;
        }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public decimal ReviewScore { get; init; }

    }
}
