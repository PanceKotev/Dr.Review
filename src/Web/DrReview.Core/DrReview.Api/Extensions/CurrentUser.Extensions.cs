namespace DrReview.Api.Extensions
{
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Auth.Models;

    public static partial class Extensions
    {
        public static IServiceCollection AddCurrentUser(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUser>(provider =>
            {
                IHttpContextAccessor httpContext = provider.GetRequiredService<IHttpContextAccessor>();

                return CurrentUser.Create(httpContext.HttpContext?.User);
            });

            return services;
        }
    }
}
