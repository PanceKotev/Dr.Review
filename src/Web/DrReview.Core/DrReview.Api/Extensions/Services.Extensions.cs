namespace DrReview.Api.Extensions
{
    using DrReview.Api.HttpClients.MojTermin.Interfaces;
    using DrReview.Api.Services;
    using DrReview.Api.Services.Interfaces;

    public static partial class Extensions
    {
        /// <summary>
        /// Registers the services for the project inside the dependency injection container.
        /// </summary>
        public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IDoctorMigrationService, DoctorMigrationService>(options =>
            {
                IMojTerminHttpClient httpClient = options.GetRequiredService<IMojTerminHttpClient>();

                return new DoctorMigrationService(
                           httpClient,
                           configuration.GetConnectionString("DatabaseConnection")!);
            });

            return services;
        }
    }
}
