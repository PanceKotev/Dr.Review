using DateOnlyTimeOnly.AspNet;
using DrReview.Api.Extensions;
WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddB2CAuthentication(builder.Configuration)
                .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
                .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());

builder.Services.RegisterCors(builder.Configuration)
                .AddSettings(builder.Configuration)
                .AddAuthorization()
                .AddHangfireConfiguration(builder.Configuration)
                .AddEmailClient()
                .AddDatabase(builder.Configuration)
                .AddUnitOfWork()
                .AddMediator()
                .AddMojTerminHttpClient(builder.Configuration)
                .AddCurrentUser()
                .AddProjectServices(builder.Configuration)
                .AddEndpointsApiExplorer()
                .AddSwagger(builder.Configuration);

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDashboard(app.Configuration);
}

app.UseRouting();

app.UseCors(builder.Configuration["CorsSettings:PolicyName"]);

app.UseAuthentication();

app.UseAuthorization();

app.UseHangfireConfiguration();

app.MapControllers();

app.Run();
