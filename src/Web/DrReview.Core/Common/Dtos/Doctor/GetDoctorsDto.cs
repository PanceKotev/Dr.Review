namespace DrReview.Common.Dtos.Doctor
{
#nullable disable
    using System.Collections.Generic;

    public class GetDoctorsDto
    {
        public GetDoctorsDto(List<GetDoctorDto> doctors, long totalCount)
        {
            Doctors = doctors;
            TotalCount = totalCount;
        }

        public List<GetDoctorDto> Doctors { get; init; }

        public long TotalCount { get; init; }
    }

    public class GetDoctorDto
    {
        public GetDoctorDto()
        {
        }

        public GetDoctorDto(string suid, string firstName, string lastName, string institution = "", string specialization = "")
        {
            Suid = suid;
            FirstName = firstName;
            LastName = lastName;
            Institution = institution;
            Specialization = specialization;
        }

        public string Suid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Institution { get; set; }

        public string Specialization { get; set; }
    }
}
