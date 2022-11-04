namespace DrReview.Api.Extensions;

using DrReview.Api.Filters;
using DrReview.Api.RecurringJobs.Services;
using global::Hangfire;
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

        services.AddScoped<IBackgroundJobClient>(sp => new BackgroundJobClient(JobStorage.Current));

        return services;
    }

    public static IApplicationBuilder UseHangfireConfiguration(this IApplicationBuilder app)
    {
        DashboardOptions options = new DashboardOptions
        {
            Authorization = new[]
            {
                    new DrReviewHangfireAuthorizationFilter()
            },
            DashboardTitle = "DrReview Hangfire",
            IgnoreAntiforgeryToken = true
        };

        app.UseHangfireDashboard("/hangfire", options);

        RecurringJobService.StartRecurringBackgroundJobs();

        return app;
    }
}
