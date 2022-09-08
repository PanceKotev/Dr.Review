#nullable disable
namespace DrReview.Contracts.ExternalApi.MojTermin.Responses
{
    using System;

    public class TimeslotsResponse
    {
        public DateTime Term { get; set; }

        public bool IsAvailable { get; set; }

        public TimeslotType TimeslotType { get; set; }
    }
}
