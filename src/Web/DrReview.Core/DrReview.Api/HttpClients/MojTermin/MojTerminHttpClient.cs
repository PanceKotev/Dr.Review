namespace DrReview.Api.HttpClients.MojTermin
{
    using System.Threading.Tasks;
    using DrReview.Api.HttpClients.MojTermin.Interfaces;

    public class MojTerminHttpClient : IMojTerminHttpClient
    {

        private readonly HttpClient _httpClient;

        public MojTerminHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetDoctorsAsync()
        {
            string path = "pp/side_navigation";
            HttpResponseMessage res = await _httpClient.GetAsync(path);

            return res;
        }

    }
}
