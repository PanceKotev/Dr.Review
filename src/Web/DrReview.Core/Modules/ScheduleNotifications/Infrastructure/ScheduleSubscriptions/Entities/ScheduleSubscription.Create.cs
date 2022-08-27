namespace DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities
{
    using System;
    using DrReview.Common.Extensions;
    using DrReview.Common.Results;

    public partial class ScheduleSubscription
    {
        public static Result<ScheduleSubscription> Create(
            long doctorFK,
            long userFK,
            DateOnly rangeFrom,
            DateOnly rangeTo)
        {
            if (!doctorFK.IsValidId() || !userFK.IsValidId())
            {
                return Result.Invalid<ScheduleSubscription>(ResultCodes.InvalidEntityReference);
            }

            if (rangeFrom > rangeTo)
            {
                return Result.Invalid<ScheduleSubscription>(ResultCodes.RangeDateFromBeforeToDate);
            }

            return Result.Ok(new ScheduleSubscription(
                                 id: default,
                                 uid: Guid.NewGuid(),
                                 deletedOn: null,
                                 modifiedOn: DateTime.UtcNow,
                                 doctorFK: doctorFK,
                                 userFK: userFK,
                                 rangeFrom: rangeFrom,
                                 rangeTo: rangeTo));
        }
    }
}
