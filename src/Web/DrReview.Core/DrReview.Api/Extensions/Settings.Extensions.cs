namespace DrReview.Api.Extensions
{
    using DrReview.Api.Settings;
    using DrReview.Common.Settings.Interfaces;

    public static partial class Extensions
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailSettings>(new EmailSettings(configuration));

            return services;
        }
    }
}
