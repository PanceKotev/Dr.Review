﻿namespace DrReview.Contracts.ExternalApi.MojTermin.Responses
{
#nullable disable
    public class DoctorResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Group { get; set; }

        public long InstitutionFK { get; set; }

        public InstitutionResponse Institution { get; set; }
    }
#nullable enable
}
