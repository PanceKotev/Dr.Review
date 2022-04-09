namespace DrReview.Common.HttpClients.MojTermin
{
    using DrReview.Common.HttpClients.MojTermin.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MojTerminHttpClient : IMojTerminHttpClient
    {

        private readonly HttpClient _httpClient;

        public MojTerminHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task GetDoctorsAsync()
        {
            var res = _httpClient.GetAsync("");


            return Task.CompletedTask;
        }

    }
}
