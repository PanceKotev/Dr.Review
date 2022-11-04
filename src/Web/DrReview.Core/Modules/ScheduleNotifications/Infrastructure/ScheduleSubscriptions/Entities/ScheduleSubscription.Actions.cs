namespace DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities
{
    using System;
    using DrReview.Common.Results;

    public partial class ScheduleSubscription
    {
        public Result<ScheduleSubscription> Delete()
        {
            this.DeletedOn = DateTime.UtcNow;

            return Result.Ok(this);
        }

        public Result<ScheduleSubscription> Update(
                                     DateOnly rangeFrom,
                                     DateOnly rangeTo)
        {
            if (rangeTo <= rangeFrom)
            {
                return Result.Invalid<ScheduleSubscription>(ResultCodes.RangeDateFromBeforeToDate);
            }

            this.RangeFrom = rangeFrom;
            this.RangeTo = rangeTo;
            this.ModifiedOn = DateTime.UtcNow;

            return Result.Ok(this);
        }
    }
}
