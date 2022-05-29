namespace DrReview.Contracts.ExternalApi.MojTermin.Responses
{
#nullable disable
    public class InstitutionResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long LocationId { get; set; } = 0;

        public List<SectionResponse> Sections { get; set; } = new List<SectionResponse>();
    }
#nullable enable
}
