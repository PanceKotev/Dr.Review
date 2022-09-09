namespace DrReview.Api.Extensions
{
    using DrReview.Common.Settings.Interfaces;
    using FluentEmail.Core;
    using FluentEmail.Razor;
    using FluentEmail.SendGrid;

    public static partial class Extensions
    {
        public static IServiceCollection AddEmailClient(this IServiceCollection services)
        {
            IEmailSettings emailSettings = services.BuildServiceProvider().GetRequiredService<IEmailSettings>();

            services
                .AddFluentEmail(emailSettings.FromEmail)
                .AddRazorRenderer(Directory.GetCurrentDirectory())
                .AddSendGridSender(emailSettings.ApiKey);

            return services;
        }
    }
}
