namespace DrReview.Common.Dtos.Doctor
{

    public class SearchDoctorDto
    {
        public SearchDoctorDto(string suid, string firstName, string lastName, string institution = "", string specialization = "")
        {
            Suid = suid;
            FirstName = firstName;
            LastName = lastName;
            Institution = institution;
            Specialization = specialization;
        }

        public string Suid { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Institution { get; init; }

        public string Specialization { get; init; }

    }
}
