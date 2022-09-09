namespace DrReview.Contracts.Dtos.Emails
{
    using System.Collections.Generic;

    public class ScheduleNotificationEmail : BaseEmailDto
    {
        public ScheduleNotificationEmail(string recipient, string subject, int numberOfFreeSlotsFound, Dictionary<DoctorScheduleNameLinkDto, List<string>> doctorSchedules)
            : base(recipient, subject)
        {
            NumberOfFreeSlotsFound = numberOfFreeSlotsFound;
            DoctorSchedules = doctorSchedules;
        }

        public int NumberOfFreeSlotsFound { get; init; }

        public Dictionary<DoctorScheduleNameLinkDto, List<string>> DoctorSchedules { get; init; }
    }

    public class DoctorScheduleNameLinkDto
    {
        public DoctorScheduleNameLinkDto(string firstName, string lastName, string link)
        {
            Name = $@"{firstName} {lastName}";
            DoctorTimeslotsLink = link;
        }

        public string Name { get; init; }

        public string DoctorTimeslotsLink { get; init; }
    }
}
