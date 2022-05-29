namespace DrReview.Api.Extensions
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Identity.Web;

    public static partial class Extensions
    {
        public static IServiceCollection AddB2CAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddMicrosoftIdentityWebApi(
                            options =>
                                {
                                    configuration.Bind("AzureAdB2C", options);

                                    options.TokenValidationParameters.NameClaimType = "name";
                                },
                            options => { configuration.Bind("AzureAdB2C", options); });

            return services;
        }
    }
}
