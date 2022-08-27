#nullable disable
namespace DrReview.Contracts.Requests
{
    using System;

    public class UpdateSubscriptionRangeRequest
    {
        public string ScheduleSuid { get; set; }

        public DateOnly RangeFrom { get; set; }

        public DateOnly RangeTo { get; set; }
    }
}
