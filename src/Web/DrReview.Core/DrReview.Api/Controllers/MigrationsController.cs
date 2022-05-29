namespace DrReview.Api.Controllers
{
    using DrReview.Api.Services.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
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
    }
}
