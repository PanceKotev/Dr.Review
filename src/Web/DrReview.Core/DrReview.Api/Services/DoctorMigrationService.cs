namespace DrReview.Api.Services
{
    using DrReview.Api.HttpClients.MojTermin.Interfaces;
    using DrReview.Api.Services.Interfaces;

    public class DoctorMigrationService : IDoctorMigrationService
    {
        private readonly IMojTerminHttpClient _mojTerminHttpClient;

        public DoctorMigrationService(IMojTerminHttpClient mojTerminHttpClient)
        {
            _mojTerminHttpClient = mojTerminHttpClient;
        }

        public Task MigrateDoctorDataAsync()
        {
            return Task.CompletedTask;
        }
    }
}
