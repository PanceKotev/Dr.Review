namespace DrReview.Api.Extensions;

using global::Hangfire;
using global::Hangfire.Dashboard.BasicAuthorization;
using global::Hangfire.SqlServer;

public static partial class Extensions
{
    public static IServiceCollection AddHangfireConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("HangfireConnection");

        services.AddHangfire(c => c
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UseSqlServerStorage(connection, new SqlServerStorageOptions
       {
           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
           QueuePollInterval = TimeSpan.Zero,
           UseRecommendedIsolationLevel = true,
           DisableGlobalLocks = true
       }));

        services.AddHangfireServer();

        return services;
    }

    public static IApplicationBuilder UseHangfireConfiguration(this IApplicationBuilder app)
    {
        var filter = new BasicAuthAuthorizationFilter(
            new BasicAuthAuthorizationFilterOptions
            {
                LoginCaseSensitive = true,
                Users = new BasicAuthAuthorizationUser[]
                {
                        new BasicAuthAuthorizationUser
                        {
                            Login = "admin",
                            PasswordClear = "password"
                        }
                }
            });

        var options = new DashboardOptions
        {
            Authorization = new[]
            {
                    filter
            }
        };

        app.UseHangfireDashboard("/hangfire", options);

        return app;
    }
}
