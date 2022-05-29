namespace DrReview.Contracts.ExternalApi.MojTermin.Responses
{
#nullable disable
    public class SectionResponse
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public List<DoctorResponse> Items { get; set; } = new List<DoctorResponse>();
    }
#nullable enable
}
