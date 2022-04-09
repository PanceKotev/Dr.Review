using DrReview.Api.Extensions;
using DrReview.Api.RecurringJobs.Services;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddHangfireConfiguration(builder.Configuration)
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireConfiguration();

app.MapControllers();

app.Run();
