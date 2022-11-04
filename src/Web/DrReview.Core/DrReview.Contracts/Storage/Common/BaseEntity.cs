namespace DrReview.Contracts.Storage.Common
{
    using System;
    using System.Text.Json.Serialization;
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

        [JsonPropertyName("ID")]
        public long Id { get; set; }

        public Guid Uid { get; set; }

        public string Suid { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime ModifiedOn { get; protected set; }
    }
}
