namespace DrReview.Contracts.Dtos
{
    using System;
    using System.Collections.Generic;

    public class GetScheduleSubscriptionsDto
    {
        public GetScheduleSubscriptionsDto(long totalCount, List<GetScheduleSubscriptionDto> subscriptions)
        {
            TotalCount = totalCount;
            Subscriptions = subscriptions;
        }

        public long TotalCount { get; init; }

        public List<GetScheduleSubscriptionDto> Subscriptions { get; init; }
    }

    public class GetScheduleSubscriptionDto
    {
        public GetScheduleSubscriptionDto(string suid, ScheduleSubscriptionRangeDto range)
        {
            Suid = suid;
            Range = range;
        }

        public string Suid { get; set; }

        public ScheduleSubscriptionRangeDto Range { get; set; }
    }

    public class ScheduleSubscriptionRangeDto
    {
        public ScheduleSubscriptionRangeDto(DateOnly from, DateOnly to)
        {
            From = from;
            To = to;
        }

        public DateOnly From { get; set; }

        public DateOnly To { get; set; }
    }
}
