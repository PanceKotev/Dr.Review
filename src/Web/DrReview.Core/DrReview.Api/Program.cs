using DrReview.Api.Extensions;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthorization()
                .AddHangfireConfiguration(builder.Configuration)
                .AddMojTerminHttpClient(builder.Configuration)
                .AddProjectServices(builder.Configuration)
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Test

app.UseAuthorization();

app.UseHangfireConfiguration();

app.MapControllers();

app.Run();
