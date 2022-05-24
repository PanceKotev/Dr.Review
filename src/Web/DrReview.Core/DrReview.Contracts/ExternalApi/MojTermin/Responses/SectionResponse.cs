namespace DrReview.Contracts.ExternalApi.MojTermin.Responses
{
    public class SectionResponse
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public List<DoctorResponse> Items { get; set; } = new List<DoctorResponse>();
    }
}
