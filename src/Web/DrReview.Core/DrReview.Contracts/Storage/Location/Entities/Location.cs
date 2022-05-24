namespace DrReview.Contracts.Storage.Location.Entities
{
    using System;
    using DrReview.Contracts.Storage.Common;

    public class Location : BaseEntity
    {
        public Location(long id, Guid uid, DateTime? deletedOn, DateTime modifiedOn, string name)
            : base(id, uid, deletedOn, modifiedOn)
        {
            Name = name;
        }

        public string Name { get; init; }
    }
}
