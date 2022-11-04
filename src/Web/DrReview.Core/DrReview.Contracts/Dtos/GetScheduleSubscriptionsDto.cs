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

    public class GetScheduleSubscriptionDoctor
    {
        public GetScheduleSubscriptionDoctor(string suid, string firstName, string lastName, string location, string institution, string specialization)
        {
            Suid = suid;
            FirstName = firstName;
            LastName = lastName;
            Location = location;
            Institution = institution;
            Specialization = specialization;
        }

        public string Suid { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Location { get; init; }

        public string Institution { get; init; }

        public string Specialization { get; init; }

    }

    public class GetScheduleSubscriptionDto
    {
        public GetScheduleSubscriptionDto(string suid, ScheduleSubscriptionRangeDto range, GetScheduleSubscriptionDoctor doctor)
        {
            Suid = suid;
            Range = range;
            Doctor = doctor;
        }

        public string Suid { get; set; }

        public ScheduleSubscriptionRangeDto Range { get; set; }

        public GetScheduleSubscriptionDoctor Doctor { get; set; }
    }

    public class ScheduleSubscriptionRangeDto
    {
        public ScheduleSubscriptionRangeDto(DateOnly from, DateOnly to, bool subscribedTo)
        {
            From = from;
            To = to;
            SubscribedTo = subscribedTo;
        }

        public ScheduleSubscriptionRangeDto(DateTime from, DateTime to, bool subscribedTo)
        {
            From = DateOnly.FromDateTime(from);
            To = DateOnly.FromDateTime(to);
            SubscribedTo = subscribedTo;
        }

        public DateOnly From { get; set; }

        public DateOnly To { get; set; }

        public bool SubscribedTo { get; set; }
    }
}
