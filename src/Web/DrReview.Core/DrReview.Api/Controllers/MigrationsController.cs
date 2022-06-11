namespace DrReview.Api.Controllers
{
    using DrReview.Api.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;

    [Route("api/[controller]")]
    [RequiredScope("drreview.read")]
    [ApiController]
    public class MigrationsController : BaseController
    {
        private readonly IDoctorMigrationService _migrationService;

        private readonly IConfiguration _configuration;

        public MigrationsController(IDoctorMigrationService migrationService, IConfiguration configuration)
        {
            _migrationService = migrationService;
            _configuration = configuration;
        }

        [HttpPost("UpdateLocations")]
        public async Task<IActionResult> MigrateLocationsAsync()
        {
            await _migrationService.PopulateLocationsAsync();

            return Ok();
        }

        [HttpPost("MigrateDoctors")]
        public async Task<IActionResult> MigrateDoctorsAsync()
        {
            await _migrationService.MigrateDoctorDataAsync();

            return Ok();
        }

        [HttpGet("TestAuth")]
        [Authorize]
        public IActionResult TestAuthentication()
        {
            return Ok();
        }


        [HttpGet]
        [Route("random")]
        public IActionResult TestRandom()
        {
            string allowedScopes = _configuration["CorsSettings:AllowedCorsOrigins"];
            string policyName = _configuration["CorsSettings:PolicyName"];
            var random = new Random();

            return Ok(random.NextInt64());
        }
    }
}
