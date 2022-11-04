namespace DrReview.Common.Auth.Interface
{
    using System;

    public interface ICurrentUser
    {
        public Guid Uid { get; }

        public string Suid { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }


    }
}
