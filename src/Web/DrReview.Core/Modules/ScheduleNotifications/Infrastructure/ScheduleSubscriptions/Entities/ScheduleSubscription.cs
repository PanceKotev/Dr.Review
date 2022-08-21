namespace DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities
{
    using System;
    using CSharpVitamins;
    using DrReview.Common.Infrastructure.Entities;

    public partial class ScheduleSubscription : AggregateRoot
    {
        public ScheduleSubscription(
            long id,
            Guid uid,
            DateTime? deletedOn,
            DateTime modifiedOn,
            long doctorFK,
            long userFK,
            DateOnly rangeFrom,
            DateOnly rangeTo)
            : base(id, uid, new ShortGuid(uid), deletedOn, modifiedOn)
        {
            DoctorFK = doctorFK;
            UserFK = userFK;
            RangeFrom = rangeFrom;
            RangeTo = rangeTo;
        }

        public long DoctorFK { get; init; }

        public long UserFK { get; init; }

        public DateOnly RangeFrom { get; private set; }

        public DateOnly RangeTo { get; private set; }
    }
}
