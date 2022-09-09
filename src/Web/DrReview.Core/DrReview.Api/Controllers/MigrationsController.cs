namespace DrReview.Api.Controllers
{
    using DrReview.Api.Services.Interfaces;
    using Hangfire;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;

    [Route("api/[controller]")]
    [RequiredScope("drreview.read")]
    public class MigrationsController : BaseController
    {
        private readonly IDoctorMigrationService _migrationService;

        private readonly IConfiguration _configuration;

        private readonly IEmailService _emailService;

        private readonly INotificationSchedulerService _notificationSchedulerService;

        public MigrationsController(
            IDoctorMigrationService migrationService,
            IConfiguration configuration,
            IEmailService emailService,
            INotificationSchedulerService notificationSchedulerService)
        {
            _migrationService = migrationService;
            _configuration = configuration;
            _emailService = emailService;
            _notificationSchedulerService = notificationSchedulerService;
        }

        [HttpPost("UpdateLocations")]
        public async Task<IActionResult> MigrateLocationsAsync()
        {
            await _migrationService.PopulateLocationsAsync();

            return Ok();
        }

        [HttpPost("SendEmail/notifications")]
        public IActionResult SendEmailNotifications()
        {
            BackgroundJob.Enqueue<INotificationSchedulerService>(x => x.SendScheduleNotificationsAsync());

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
