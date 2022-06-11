namespace DrReview.Api.Extensions
{
    public static partial class Extensions
    {
        public static IServiceCollection RegisterCors(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddCors(options =>
            {
                options.AddPolicy(configuration["CorsSettings:PolicyName"], policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            return serviceCollection;

        }
    }
}
