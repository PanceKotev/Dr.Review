namespace DrReview.Modules.User.Infrastructure.User.Entities
{
    using System;
    using System.Collections.Generic;
    using DrReview.Common.Infrastructure.Entities;
    using DrReview.Common.Mediator.Contracts;

    public partial class User : AggregateRoot
    {
        private User(long id, Guid uid, string suid, DateTime? deletedOn, DateTime modifiedOn, string firstName, string lastName, string emailAddress) 
            : base(id, uid, suid, deletedOn, modifiedOn)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string EmailAddress { get; init; }


    }
}
