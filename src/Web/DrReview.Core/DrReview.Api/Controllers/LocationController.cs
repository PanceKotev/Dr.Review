namespace DrReview.Api.Controllers
{
    using DrReview.Api.Services.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : BaseController
    {
        private readonly IDoctorMigrationService _migrationService;

        public LocationController(IDoctorMigrationService migrationService)
        {
            _migrationService = migrationService;
        }

        [HttpGet("UpdateLocations")]
        public async Task<IActionResult> MigrateLocationsAsync()
        {
            await _migrationService.PopulateLocationsAsync();

            return Ok();
        }
    }
}
