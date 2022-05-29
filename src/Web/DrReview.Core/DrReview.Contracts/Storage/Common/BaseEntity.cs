namespace DrReview.Contracts.Storage.Common
{
    using System;
    using CSharpVitamins;

    public class BaseEntity
    {
        public BaseEntity(long id, Guid uid, DateTime? deletedOn, DateTime modifiedOn)
        {
            Id = id;
            Uid = uid;
            Suid = new ShortGuid(uid);
            DeletedOn = deletedOn;
            ModifiedOn = modifiedOn;
        }

        public long Id { get; init; }

        public Guid Uid { get; init; }

        public string Suid { get; init; }

        public DateTime? DeletedOn { get; init; }

        public DateTime ModifiedOn { get; init; }
    }
}
