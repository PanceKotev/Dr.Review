namespace DrReview.Contracts.ExternalApi.MojTermin.Responses
{
    public class MojTerminResourceResponse
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public int Levels { get; set; }

        public long Id { get; set; }

        public long LocationId { get; set; } = 0L;

        public List<MojTerminResourceResponse> Subsections { get; set; } = new List<MojTerminResourceResponse>();
    }
}
