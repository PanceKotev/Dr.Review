namespace DrReview.Contracts.Dtos.Emails
{
    using System.Collections.Generic;

    public class ScheduleNotificationEmail : BaseEmailDto
    {
        public ScheduleNotificationEmail(string recipient, string subject, int numberOfFreeSlotsFound, Dictionary<string, List<string>> doctorSchedules)
            : base(recipient, subject)
        {
            NumberOfFreeSlotsFound = numberOfFreeSlotsFound;
            DoctorSchedules = doctorSchedules;
        }

        public int NumberOfFreeSlotsFound { get; init; }

        public Dictionary<string, List<string>> DoctorSchedules { get; init; }
    }
}
