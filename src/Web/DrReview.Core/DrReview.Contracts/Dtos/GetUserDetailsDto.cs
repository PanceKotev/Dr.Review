namespace DrReview.Contracts.Dtos
{

    public class GetUserDetailsDto
    {
        public GetUserDetailsDto(string suid, string firstName, string lastName, string email)
        {
            Suid = suid;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string Suid { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string FullName { get { return $@"{FirstName} {LastName}"; } }

        public string Email { get; init; }
    }
}
