namespace DrReview.Api.Services
{
    using DrReview.Api.HttpClients.MojTermin.Contracts;
    using DrReview.Api.HttpClients.MojTermin.Interfaces;
    using DrReview.Api.Services.Interfaces;

    public class DoctorMigrationService : IDoctorMigrationService
    {
        private readonly IMojTerminHttpClient _mojTerminHttpClient;

        public DoctorMigrationService(IMojTerminHttpClient mojTerminHttpClient)
        {
            _mojTerminHttpClient = mojTerminHttpClient;
        }

        public async Task MigrateDoctorDataAsync()
        {
            List<long> res = await _mojTerminHttpClient.GetInstitutionsAsync();

            int maxRequests = 35;
            int maxPages = 5;

            List<DoctorResponse> doctors = new List<DoctorResponse>();

            for (int page = 0; page < maxPages; page++)
            {
                long[] institutionIds = res.GetRange(page * maxRequests, maxRequests).ToArray();
                doctors.AddRange(_mojTerminHttpClient.GetDoctorsInInstitutions(institutionIds));

            }


        }
    }
}
