namespace DrReview.Common.Dtos.Doctor
{
    using DrReview.Contracts.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GetDoctorDetailsDto
    {
        public GetDoctorDetailsDto(
            string suid,
            string firstName,
            string lastName,
            string institution = "",
            string specialization = "",
            string location = "",
            GetDoctorDetailsScheduleSubscriptionDto? scheduleSubscription = null)
        {
            Suid = suid;
            FirstName = firstName;
            LastName = lastName;
            Institution = institution;
            Specialization = specialization;
            Location = location;
            ScheduleSubscription = scheduleSubscription;
        }

        public GetDoctorDetailsDto(
            string suid,
            string firstName,
            string lastName,
            string institution = "",
            string specialization = "",
            string location = "")
        {
            Suid = suid;
            FirstName = firstName;
            LastName = lastName;
            Institution = institution;
            Specialization = specialization;
            Location = location;
            ScheduleSubscription = null;
        }

        public string Suid { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Institution { get; init; }

        public string Specialization { get; init; }

        public string Location { get; init; }

        public GetDoctorDetailsScheduleSubscriptionDto? ScheduleSubscription { get; set; }
    }

    public class GetDoctorDetailsScheduleSubscriptionDto
    {
        public GetDoctorDetailsScheduleSubscriptionDto(string scheduleSuid, DateTime from, DateTime to, bool subscribedTo)
        {
            ScheduleSuid = scheduleSuid;
            Range = new ScheduleSubscriptionRangeDto(from, to, subscribedTo);
        }

        public ScheduleSubscriptionRangeDto Range { get; init; }

        public string ScheduleSuid { get; init; }

    }
}
