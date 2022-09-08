namespace DrReview.Api.HttpClients.MojTermin
{
    using System.Threading.Tasks;
    using DrReview.Api.HttpClients.MojTermin.Interfaces;
    using DrReview.Contracts.ExternalApi.MojTermin.Responses;

    public class MojTerminHttpClient : IMojTerminHttpClient
    {
        private readonly HttpClient _httpClient;

        public MojTerminHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Dictionary<long, long>> GetInstitutionsAsync()
        {
            string path = "pp/side_navigation";
            HttpResponseMessage res = await _httpClient.GetAsync(path);

            MojTerminResourceResponse responseResult = (await res.Content.ReadFromJsonAsync<MojTerminResourceResponse[]>())![2];

            List<MojTerminResourceResponse> institutions = responseResult.Subsections.SelectMany(d => d.Subsections).ToList();

            Dictionary<long, long> institutionLocationIdsMap = new Dictionary<long, long>();

            institutions.ForEach(d =>
            {
                if (!institutionLocationIdsMap.ContainsKey(d.Id))
                {
                    institutionLocationIdsMap[d.Id] = d.LocationId;
                }
            });

            return institutionLocationIdsMap;
        }

        public async Task<List<LocationResponse>> GetLocationsAsync()
        {
            string path = "pp/locations";

            HttpResponseMessage res = await _httpClient.GetAsync(path);

            Dictionary<int, LocationResponse>? responseResult = await res.Content.ReadFromJsonAsync<Dictionary<int, LocationResponse>>();

            if (responseResult is null)
            {
                throw new ArgumentException("Cannot convert locations");
            }

            return responseResult.Values.ToList();
        }

        public List<DoctorResponse> GetDoctorsInInstitutions(Dictionary<long, long> institutionIdLocationMap)
        {
            string path = "pp/institutions/";

            long[] institutionsIds = institutionIdLocationMap.Keys.ToArray();

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

            List<InstitutionResponse> institutionResults = convertTasks.Select(x => x.Result!).ToList();

            List<DoctorResponse> allDoctors = new List<DoctorResponse>();

            foreach (InstitutionResponse result in institutionResults)
            {
                result.LocationId = institutionIdLocationMap[result.Id];

                SectionResponse doctorSection = result.Sections.First(s => s.Type == "doctor");

                doctorSection.Items.ForEach(x =>
                {
                    x.InstitutionFK = result.Id;
                    x.Institution = result;
                });

                allDoctors.AddRange(doctorSection.Items);
            }

            return allDoctors;
        }

        public async Task<List<TimeslotDoctorResponse>> GetTimeslotsForDoctorsAsync(List<long> doctorIds)
        {
            string path = "pp/resources/";

            List<Task<TimeslotDoctorResponse>> timeslots = new ();

            foreach (long doctorId in doctorIds)
            {
                timeslots.Add(_httpClient.GetFromJsonAsync<TimeslotDoctorResponse>($@"{path}{doctorId}/slots_availability")!);
            }

            List<TimeslotDoctorResponse> results = (await Task.WhenAll(timeslots)).ToList();

            List<TimeslotDoctorResponse> filteredResults = results.Where(r => r.Timeslots.Any(t => t.Value.Any(y => y.IsAvailable && y.TimeslotType != TimeslotType.BUSY))).ToList();

            return filteredResults;
        }
    }
}
