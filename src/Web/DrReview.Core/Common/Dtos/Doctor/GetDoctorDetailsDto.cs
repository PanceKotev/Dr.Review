namespace DrReview.Common.Dtos.Doctor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GetDoctorDetailsDto
    {
        public GetDoctorDetailsDto(string suid, string firstName, string lastName, string institution = "", string specialization = "", string location = "")
        {
            Suid = suid;
            FirstName = firstName;
            LastName = lastName;
            Institution = institution;
            Specialization = specialization;
            Location = location;
        }

        public string Suid { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Institution { get; init; }

        public string Specialization { get; init; }

        public string Location { get; init; }
    }
}
