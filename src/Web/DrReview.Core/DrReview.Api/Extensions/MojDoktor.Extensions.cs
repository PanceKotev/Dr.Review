namespace DrReview.Api.Extensions
{
    using DrReview.Api.HttpClients.MojTermin;
    using DrReview.Api.HttpClients.MojTermin.Interfaces;

    public static partial class Extensions
    {
        /// <summary>
        /// Register the MojTermin http client in the dependency injection container.
        /// </summary>
        /// <param name="services">The services to add the moj termin http client in.</param>
        /// <param name="configuration">The configuration for this project.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddMojTerminHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IMojTerminHttpClient, MojTerminHttpClient>(options =>
            {
                options.BaseAddress = new Uri(configuration["MojTermin:BaseUrl"]);
            });

            return services;
        }
    }
}
