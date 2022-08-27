#nullable disable
namespace DrReview.Contracts.Requests
{
    using System;

    public class SubscribeToDoctorsScheduleRequest
    {
        public string DoctorSuid { get; set; }

        public DateOnly RangeFrom { get; set; }

        public DateOnly RangeTo { get; set; }
    }
}
