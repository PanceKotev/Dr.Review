#nullable disable
namespace DrReview.Contracts.ExternalApi.MojTermin.Responses
{
    using System;
    using System.Collections.Generic;

    public class TimeslotDoctorResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public Dictionary<string, List<TimeslotsResponse>> Timeslots { get; set; } = new ();
    }
}
