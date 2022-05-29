namespace DrReview.Api.Extensions
{
    using Microsoft.OpenApi.Models;

    public static partial class Extensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Dr Review API", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "OAuth2.0 Auth Code with PKCE",
                    Name = "oauth2",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(configuration["SwaggerSettings:AuthorizationUrl"]),
                            TokenUrl = new Uri(configuration["SwaggerSettings:TokenUrl"]),
                            Scopes = new Dictionary<string, string>
                            {
                                { configuration["SwaggerSettings:ApiScopes"], "read the api" }
                            }
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { configuration["SwaggerSettings:ApiScopes"] }
                    }
                });
            });

            return services;
        }

        public static WebApplication UseSwaggerDashboard(this WebApplication app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger DrReview Api v1");
                c.OAuthClientId(configuration["SwaggerSettings:ClientId"]);
                c.OAuthUsePkce();
                c.OAuthScopeSeparator(" ");
            });

            return app;
        }
    }
}
