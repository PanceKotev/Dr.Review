namespace DrReview.Api.RecurringJobs.Services
{
    using DrReview.Api.Services.Interfaces;
    using Hangfire;

    public static class RecurringJobService
    {
        public static void StartRecurringBackgroundJobs()
        {
            StarDoctorUpdatingService();
        }

        [AutomaticRetry(Attempts = 2, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        private static void StarDoctorUpdatingService()
        {
            RecurringJob.AddOrUpdate<IDoctorMigrationService>(
                migrationService => migrationService.MigrateDoctorDataAsync(),
                Cron.Weekly());
        }

    }
}
