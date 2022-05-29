namespace DrReview.Api.Services.Interfaces
{
    public interface IDoctorMigrationService
    {
        Task MigrateDoctorDataAsync();

        Task PopulateLocationsAsync();
    }
}
