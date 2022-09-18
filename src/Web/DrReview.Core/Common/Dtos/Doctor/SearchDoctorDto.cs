namespace DrReview.Common.Dtos.Doctor
{
#nullable disable

    public class SearchDoctorDto
    {
        public SearchDoctorDto()
        {
        }

        public SearchDoctorDto(string suid, string firstName, string lastName, string institution = "", string specialization = "")
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
