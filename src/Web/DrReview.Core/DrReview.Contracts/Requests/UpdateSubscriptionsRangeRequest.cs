#nullable disable
namespace DrReview.Contracts.Requests
{
    using System;

    public class UpdateSubscriptionsRangeRequest
    {
        public List<string> ScheduleSuids { get; set; }

        public DateOnly RangeFrom { get; set; }

        public DateOnly RangeTo { get; set; }
    }
}
