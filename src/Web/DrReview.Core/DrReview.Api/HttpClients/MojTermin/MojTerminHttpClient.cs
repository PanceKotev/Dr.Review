﻿namespace DrReview.Api.HttpClients.MojTermin
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

        public async Task<List<long>> GetInstitutionsAsync()
        {
            string path = "pp/side_navigation";
            HttpResponseMessage res = await _httpClient.GetAsync(path);

            MojTerminResourceResponse responseResult = (await res.Content.ReadFromJsonAsync<MojTerminResourceResponse[]>())![2];

            List<MojTerminResourceResponse> doctors = responseResult.Subsections.SelectMany(d => d.Subsections).ToList();

            List<long> institutionIds = doctors.Select(x => x.Id).ToList();

            string joinedIds = string.Join('\n', institutionIds.Select(x => x.ToString()).ToArray());

            return institutionIds;
        }

        public List<DoctorResponse> GetDoctorsInInstitutions(long[] institutionsIds)
        {
            string path = "pp/institutions/";

            Task<HttpResponseMessage>[] tasks = new Task<HttpResponseMessage>[institutionsIds.Length];

            for (int i = 0; i < institutionsIds.Length; i++)
            {
                tasks[i] = _httpClient.GetAsync($@"{path}{institutionsIds[i]}");
            }

            Task.WaitAll(tasks);

            var res = tasks.Select(x => x.Result).ToList();

            Task<InstitutionResponse?>[] convertTasks = new Task<InstitutionResponse?>[tasks.Length];

            for (int i = 0; i < convertTasks.Length; i++)
            {
                convertTasks[i] = res[i].Content.ReadFromJsonAsync<InstitutionResponse>();
            }

            Task.WaitAll(convertTasks);

            var returnedDoctors = convertTasks.Select(x => x.Result).ToList();

            var allDoctors = returnedDoctors.SelectMany(x => x.Sections.First(x => x.Type == "doctor").Items).ToList();

            return allDoctors;
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
