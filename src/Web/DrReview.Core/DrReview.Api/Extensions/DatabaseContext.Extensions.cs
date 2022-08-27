namespace DrReview.Api.Extensions
{
    using DrReview.Modules.Review.Infrastructure.Common.Contexts;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.Contexts;
    using DrReview.Modules.User.Infrastructure.Common.Contexts;
    using Microsoft.EntityFrameworkCore;

    public static partial class Extensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<UserDatabaseContext>(options =>
            options.UseSqlServer(
               configuration.GetConnectionString("DatabaseConnection"),
               x => x.EnableRetryOnFailure())
           .LogTo(Console.WriteLine, LogLevel.Debug));

            services.AddDbContextPool<UserReadOnlyDatabaseContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DatabaseReadonlyConnection"),
                    x => x.EnableRetryOnFailure())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                .LogTo(Console.WriteLine, LogLevel.Debug));

            services.AddDbContextPool<ReviewDatabaseContext>(options =>
                options.UseSqlServer(
                   configuration.GetConnectionString("DatabaseConnection"),
                   x => x.EnableRetryOnFailure())
                .LogTo(Console.WriteLine, LogLevel.Debug));

            services.AddDbContextPool<ReviewReadOnlyDatabaseContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DatabaseReadonlyConnection"),
                    x => x.EnableRetryOnFailure())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                .LogTo(Console.WriteLine, LogLevel.Debug));

            services.AddDbContextPool<ScheduleNotificationDatabaseContext>(options =>
                options.UseSqlServer(
                   configuration.GetConnectionString("DatabaseConnection"),
                   x => x.EnableRetryOnFailure())
                .LogTo(Console.WriteLine, LogLevel.Debug));

            services.AddDbContextPool<ScheduleNotificationReadonlyDatabaseContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DatabaseReadonlyConnection"),
                    x => x.EnableRetryOnFailure())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                .LogTo(Console.WriteLine, LogLevel.Debug));

            return services;
        }
    }
}
