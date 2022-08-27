#nullable disable
namespace DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities
{
    using System;
    using CSharpVitamins;
    using DrReview.Common.Infrastructure.Entities;

    public class Doctor : BaseEntity
    {
        public Doctor(long id, Guid uid, DateTime? deletedOn, DateTime modifiedOn, string firstName, string lastName)
         : base(id, uid, new ShortGuid(uid), deletedOn, modifiedOn)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}
