namespace DrReview.Api.Settings
{
    using DrReview.Common.Settings.Interfaces;

    public class EmailSettings : SettingsBase, IEmailSettings
    {
        public EmailSettings(IConfiguration configuration)
        : base(configuration)
        {
        }

        public string FromEmail => GetValue<string>("EmailSettings:FromEmail");

        public string ApiKey =>
#if DEBUG
            Environment.GetEnvironmentVariable("DRREVIEW_SENDGRID_API_KEY")!;
#else
GetValue<string>("EmailSettings:ApiKey");
#endif
    }
}
