namespace DrReview.Api.Extensions
{
    using DrReview.Modules.User.Infrastructure.Common.UnitOfWork;
    using DrReview.Modules.User.Infrastructure.Common.UnitOfWork.Interfaces;

    public static partial class Extensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

            return services;
        }
    }
}
