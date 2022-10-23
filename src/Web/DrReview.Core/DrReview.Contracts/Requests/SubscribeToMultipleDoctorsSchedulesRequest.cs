#nullable disable
namespace DrReview.Contracts.Requests
{
    using System;

    public class SubscribeToMultipleDoctorsSchedulesRequest
    {
        public List<string> DoctorSuids { get; set; }

        public DateOnly RangeFrom { get; set; }

        public DateOnly RangeTo { get; set; }
    }
}
