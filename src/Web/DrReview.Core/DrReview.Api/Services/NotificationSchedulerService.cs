namespace DrReview.Api.Services
{
    using System.Linq;
    using DrReview.Api.HttpClients.MojTermin.Interfaces;
    using DrReview.Api.Services.Interfaces;
    using DrReview.Contracts.Dtos.Emails;
    using DrReview.Contracts.ExternalApi.MojTermin.Responses;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.Contexts;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using Microsoft.EntityFrameworkCore;

    public class NotificationSchedulerService : INotificationSchedulerService
    {
        private readonly ScheduleNotificationReadonlyDatabaseContext _databaseContext;

        private readonly IEmailService _emailService;

        private readonly IMojTerminHttpClient _mojTerminHttpClient;

        public NotificationSchedulerService(
            ScheduleNotificationReadonlyDatabaseContext databaseContext,
            IEmailService emailService,
            IMojTerminHttpClient mojTerminHttpClient)
        {
            _databaseContext = databaseContext;
            _emailService = emailService;
            _mojTerminHttpClient = mojTerminHttpClient;
        }

        public async Task SendScheduleNotificationsAsync()
        {
            DateOnly dateNow = DateOnly.FromDateTime(DateTime.UtcNow);

            List<ScheduleSubscription> allScheduleNotifications = await _databaseContext.ScheduleSubscriptions
                                                                                        .Where(s => s.ScheduleSubscriber != null && s.RangeTo > dateNow)
                                                                                        .Include(s => s.ScheduleSubscriber)
                                                                                        .Include(s => s.Doctor)
                                                                                        .ToListAsync();
            if (!allScheduleNotifications.Any())
            {
                return;
            }

            Dictionary<string, List<ScheduleSubscription>> allScheduleNotificationsGrouped = allScheduleNotifications
                                                                                                                    .GroupBy(x => x.ScheduleSubscriber!.Email)
                                                                                                                    .ToDictionary(x => x.Key, x => x.ToList());

            var doctorSchedules = (await _mojTerminHttpClient.GetTimeslotsForDoctorsAsync(allScheduleNotifications.Select(s => s.DoctorFK).ToList()))
                                                             .GroupBy(d => d.Id)
                                                             .ToDictionary(x => x.Key, x => x.ToList());
            List<Task> emailsToSend = new ();

            foreach (KeyValuePair<string, List<ScheduleSubscription>> entry in allScheduleNotificationsGrouped)
            {
                var drSchedules = entry.Value.GroupBy(x => $@"{x.Doctor!.FirstName} - {x.Doctor!.LastName}")
                                             .ToDictionary(x => x.Key, x => x.Select(x => x.RangeTo.ToString()).ToList());

#pragma warning disable S1481 // Unused local variables should be removed
                List<Contracts.ExternalApi.MojTermin.Responses.TimeslotDoctorResponse>? mojTerminResults = await _mojTerminHttpClient.GetTimeslotsForDoctorsAsync(entry.Value.Select(x => x.DoctorFK).ToList());
#pragma warning restore S1481 // Unused local variables should be removed

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
