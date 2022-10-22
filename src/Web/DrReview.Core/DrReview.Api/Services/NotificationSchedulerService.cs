namespace DrReview.Api.Services
{
    using System.Linq;
    using DrReview.Api.HttpClients.MojTermin.Interfaces;
    using DrReview.Api.Services.Interfaces;
    using DrReview.Contracts.Dtos.Emails;
    using DrReview.Contracts.ExternalApi.MojTermin.Responses;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.Contexts;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using FluentEmail.Core.Models;
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
                                                             .ToDictionary(x => x.Key, x => x.FirstOrDefault());
            List<ScheduleNotificationEmail> emailDtos = new List<ScheduleNotificationEmail>();

            foreach (KeyValuePair<string, List<ScheduleSubscription>> entry in allScheduleNotificationsGrouped)
            {
                var drSchedules = entry.Value.GroupBy(x => x.Doctor!)
                                             .ToDictionary(x => x.Key, x => x.ToList());

                Dictionary<DoctorScheduleNameLinkDto, List<string>> finalSchedules = new Dictionary<DoctorScheduleNameLinkDto, List<string>>();

                foreach (KeyValuePair<Doctor, List<ScheduleSubscription>> drEntry in drSchedules)
                {
                    TimeslotDoctorResponse? schedulesForDoctor = doctorSchedules.GetValueOrDefault(drEntry.Key.Id);

                    if (schedulesForDoctor is null || !schedulesForDoctor.Timeslots.Any())
                    {
                        continue;
                    }

                    List<DateTime> allTerms = schedulesForDoctor.Timeslots.SelectMany(x => x.Value.Where(y => y.IsAvailable && y.TimeslotType != TimeslotType.BUSY).Select(y => y.Term)).ToList();

                    if (!allTerms.Any())
                    {
                        continue;
                    }

                    List<string> validTimeSlots = allTerms
                                                                    .Where(x => drEntry.Value.Any(d => dateNow <= DateOnly.FromDateTime(x)
                                                                                                            && d.RangeFrom <= DateOnly.FromDateTime(x)
                                                                                                            && DateOnly.FromDateTime(x) <= d.RangeTo))
                                                                    .Select(x => x.ToString("dd/MM/yyyy HH:mm")).ToList();

                    if (!validTimeSlots.Any())
                    {
                        continue;
                    }

                    string doctorLink = "http://mojtermin.mk/map/specijalist?resource=";
                    finalSchedules.Add(new DoctorScheduleNameLinkDto(drEntry.Key.FirstName, drEntry.Key.LastName, $@"{doctorLink}{drEntry.Key.Id}"), validTimeSlots);
                }

                if (!finalSchedules.Any())
                {
                    continue;
                }

                ScheduleNotificationEmail emailDto = new ScheduleNotificationEmail(
                    recipient: entry.Key,
                    subject: $@"Слободни термини за доктори {dateNow.ToString("dd/MM/yyyy")}",
                    numberOfFreeSlotsFound: finalSchedules.Count,
                    doctorSchedules: finalSchedules);

                emailDtos.Add(emailDto);
            }
            if (!emailDtos.Any())
            {
                return;
            }
            List<Task<SendResponse>> emailsToSend = emailDtos.Select(emailDto =>
            { 
                return _emailService.SendEmailAsync(emailDto);
            }).ToList();

            await Task.WhenAll(emailsToSend);
        }
    }
}
