namespace DrReview.Common.Dtos.Doctor
{
#nullable disable

    public class GetTopDoctorsDto
    {
        public GetTopDoctorsDto()
        {
        }

        public GetTopDoctorsDto(
            string suid,
            string firstName,
            string lastName,
            string institution,
            string specialization,
            string location,
            int distance)
        {
            Suid = suid;
            FirstName = firstName;
            LastName = lastName;
            Institution = institution;
            Specialization = specialization;
            Location = location;
            Distance = distance;
        }

        public string Suid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Institution { get; set; }

        public string Specialization { get; set; }

        public string Location { get; set; }

        public int Distance { get; set; }
    }
}
