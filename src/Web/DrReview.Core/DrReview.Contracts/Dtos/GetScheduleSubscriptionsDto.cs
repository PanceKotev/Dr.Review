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

        public string Suid { get; init; }

        public ScheduleSubscriptionRangeDto Range { get; init; }
    }

    public class ScheduleSubscriptionRangeDto
    {
        public ScheduleSubscriptionRangeDto(DateOnly from, DateOnly to, bool subscribedTo)
        {
            From = from;
            To = to;
            SubscribedTo = subscribedTo;
        }

        public DateOnly From { get; init; }

        public DateOnly To { get; init; }

        public bool SubscribedTo { get; init; }
    }
}
