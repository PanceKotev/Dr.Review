namespace DrReview.Api.Extensions
{
    using DrReview.Modules.User.Infrastructure.Common.Contexts;
    using DrReview.Modules.User.Infrastructure.Common.UnitOfWork;
    using DrReview.Modules.User.Infrastructure.Common.UnitOfWork.Interfaces;
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

            services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

            return services;
        }
    }
}
