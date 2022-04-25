namespace DrReview.Api.HttpClients.MojTermin
{
    using System.Threading.Tasks;
    using DrReview.Api.HttpClients.MojTermin.Contracts;
    using DrReview.Api.HttpClients.MojTermin.Interfaces;
    using DrReview.Common.Results;
    using Newtonsoft.Json.Linq;

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

            MojTerminResourceResponse responseResult = (await res.Content.ReadFromJsonAsync<MojTerminResourceResponse[]>())![0];

            List<MojTerminResourceResponse> doctors = responseResult.Subsections.SelectMany(d => d.Subsections.SelectMany(dr => dr.Subsections.SelectMany(dro => dro.Subsections))).ToList();

            return res;
        }


        private async Task<Result> HandleResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {

            }


            return Result.Invalid("no");
        }

    }
}
