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
    }
}
