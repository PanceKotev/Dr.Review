namespace DrReview.Common.Auth.Models
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Security.Claims;
    using CSharpVitamins;
    using DrReview.Common.Auth.Interface;

    public class CurrentUser : ICurrentUser
    {
        private const string _scopeUid = "nameidentifier";
        private const string _scopeFirstName = "givenname";
        private const string _scopeLastName = "surname";
        private const string _scopeEmail = "emailaddress";
        private const string _schema = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/";
        private static readonly Uri SchemasPath = new Uri(_schema);

        public Guid Uid { get; }

        public string Suid { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        private CurrentUser(Guid uid, string firstName, string lastName, string email)
        {
            Uid = uid;
            Suid = new ShortGuid(uid);
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public static CurrentUser Create(ClaimsPrincipal? claimsPrincipal)
        {
            if (claimsPrincipal == null || !claimsPrincipal.Claims.Any())
            {
                return new CurrentUser(Guid.Empty, string.Empty, string.Empty, string.Empty);
            }

            string email;

            email = new MailAddress(GetFromClaims(claimsPrincipal, _scopeEmail)).ToString();

            if (!Guid.TryParse(GetFromClaims(claimsPrincipal, _scopeUid), out Guid uid))
            {
                throw new InvalidDataException("Invalid user uid");
            }

            string firstName = GetFromClaims(claimsPrincipal, _scopeFirstName);

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new InvalidDataException("Invalid first name");
            }

            string lastName = GetFromClaims(claimsPrincipal, _scopeLastName);

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new InvalidDataException("Invalid last name");
            }

            return new CurrentUser(uid, firstName, lastName, email);
        }

        private static string GetFromClaims(ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal.FindFirst($"{SchemasPath.AbsoluteUri}{claimType}")?.Value ?? string.Empty;
        }
    }
}
