namespace DrReview.Api.Settings
{
    using Microsoft.Extensions.Configuration;

    public abstract class SettingsBase
    {
        private readonly IConfiguration _configuration;

        protected SettingsBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected T GetValue<T>(string key)
        {
            return _configuration.GetValue<T>(key);
        }

        protected T GetSection<T>(string key)
        {
            return _configuration.GetSection(key).Get<T>();
        }

        protected string GetConnectionString(string key)
        {
            return _configuration.GetConnectionString(key);
        }
    }
}
