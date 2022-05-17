namespace DrReview.Contracts.ExternalApi.MojTermin.Responses
{
    public class InstitutionResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<SectionResponse> Sections { get; set; } = new List<SectionResponse>();
    }
}
