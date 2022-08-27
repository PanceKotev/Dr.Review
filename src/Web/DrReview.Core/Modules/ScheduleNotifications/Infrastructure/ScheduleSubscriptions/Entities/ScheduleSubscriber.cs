#nullable disable
namespace DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities
{
    using System;
    using CSharpVitamins;
    using DrReview.Common.Infrastructure.Entities;

    public class ScheduleSubscriber : BaseEntity
    {
        public ScheduleSubscriber(long id, Guid uid, DateTime? deletedOn, DateTime modifiedOn, string firstName, string lastName, string email)
         : base(id, uid, new ShortGuid(uid), deletedOn, modifiedOn)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Email { get; init; }
    }
}
