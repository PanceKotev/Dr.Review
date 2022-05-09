namespace DrReview.Api.Controllers;

using System.Data.SqlClient;
using DrReview.Api.HttpClients.MojTermin.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly IMojTerminHttpClient _mojTerminHttpClient;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IMojTerminHttpClient mojTerminHttpClient)
    {
        _logger = logger;
        _mojTerminHttpClient = mojTerminHttpClient;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost(Name = "PostDoctors")]
    public IActionResult PostDoctors([FromBody] long[] institutionIds)
    {
        _mojTerminHttpClient.GetDoctorsInInstitutions(institutionIds);

        return Ok();
    }

}
