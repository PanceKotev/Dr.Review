namespace DrReview.Contracts.Storage.Specialization.Entities
{
    using System;
    using DrReview.Contracts.Storage.Common;

    public class Specialization : BaseEntity
    {
        public Specialization(long id, Guid uid, DateTime? deletedOn, DateTime modifiedOn, string name)
            : base(id, uid, deletedOn, modifiedOn)
        {
            Name = name;
        }

        public string Name { get; set; }

        public static Specialization FromName(string specializationName)
        {
            return new Specialization(
                id: default,
                uid: Guid.NewGuid(),
                deletedOn: null,
                modifiedOn: DateTime.UtcNow,
                name: specializationName);
        }
    }
}
