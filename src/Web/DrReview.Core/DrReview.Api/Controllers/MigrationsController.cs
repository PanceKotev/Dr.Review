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

        public MigrationsController(IDoctorMigrationService migrationService)
        {
            _migrationService = migrationService;
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
    }
}
