namespace DrReview.Api.Services
{
    using System.Linq;
    using DrReview.Api.Services.Interfaces;
    using DrReview.Contracts.Dtos.Emails;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.Contexts;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using Microsoft.EntityFrameworkCore;

    public class NotificationSchedulerService : INotificationSchedulerService
    {
        private readonly ScheduleNotificationReadonlyDatabaseContext _databaseContext;

        private readonly IEmailService _emailService;

        public NotificationSchedulerService(ScheduleNotificationReadonlyDatabaseContext databaseContext, IEmailService emailService)
        {
            _databaseContext = databaseContext;
            _emailService = emailService;
        }

        public async Task SendScheduleNotificationsAsync()
        {
            DateOnly dateNow = DateOnly.FromDateTime(DateTime.UtcNow);

            List<ScheduleSubscription> allScheduleNotifications = await _databaseContext.ScheduleSubscriptions
                                                                                        .Where(s => s.ScheduleSubscriber != null && s.RangeTo > dateNow)
                                                                                        .Include(s => s.ScheduleSubscriber)
                                                                                        .Include(s => s.Doctor)
                                                                                        .ToListAsync();

            Dictionary<string, List<ScheduleSubscription>> allScheduleNotificationsGrouped = allScheduleNotifications
                                                                                                                    .GroupBy(x => x.ScheduleSubscriber!.Email)
                                                                                                                    .ToDictionary(x => x.Key, x => x.ToList());
            List<Task> emailsToSend = new ();

            foreach (KeyValuePair<string, List<ScheduleSubscription>> entry in allScheduleNotificationsGrouped)
            {
                var drSchedules = entry.Value.GroupBy(x => $@"{x.Doctor!.FirstName} - {x.Doctor!.LastName}")
                                             .ToDictionary(x => x.Key, x => x.Select(x => x.RangeTo.ToString()).ToList());

                ScheduleNotificationEmail emailDto = new ScheduleNotificationEmail(
                    recipient: entry.Key,
                    subject: "Слободни термини за доктори",
                    numberOfFreeSlotsFound: entry.Value.Count,
                    doctorSchedules: drSchedules);

                emailsToSend.Add(_emailService.SendEmailAsync(emailDto));
            }

            await Task.WhenAll(emailsToSend);
        }
    }
}
