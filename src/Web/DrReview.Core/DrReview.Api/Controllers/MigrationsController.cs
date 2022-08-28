namespace DrReview.Api.Controllers
{
    using DrReview.Api.Services.Interfaces;
    using DrReview.Contracts.Dtos.Emails;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;

    [Route("api/[controller]")]
    [RequiredScope("drreview.read")]
    public class MigrationsController : BaseController
    {
        private readonly IDoctorMigrationService _migrationService;

        private readonly IConfiguration _configuration;

        private readonly IEmailService _emailService;

        public MigrationsController(IDoctorMigrationService migrationService, IConfiguration configuration, IEmailService emailService)
        {
            _migrationService = migrationService;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost("UpdateLocations")]
        public async Task<IActionResult> MigrateLocationsAsync()
        {
            await _migrationService.PopulateLocationsAsync();

            return Ok();
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmailAsync()
        {
            await _emailService.SendEmailAsync(new TestEmail(
                                    recipient: "avreinix@gmail.com",
                                    subject: "Test Email",
                                    testProperty: "TEEEEEEEEEEEEEEEEEST"));

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
