namespace DrReview.Api.HttpClients.MojTermin.Contracts
{
    public class MojTerminResourceResponse
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public int Levels { get; set; }

        public long Id { get; set; }

        public List<MojTerminResourceResponse> Subsections { get; set; } = new List<MojTerminResourceResponse>();
    }
}
