namespace DrReview.Modules.User.Infrastructure.User.Entities
{
    using System;
    using System.Collections.Generic;
    using DrReview.Common.Infrastructure.Entities;
    using DrReview.Common.Mediator.Contracts;

    public partial class User : AggregateRoot
    {
        private User(long id, Guid uid, string suid, DateTime? deletedOn, DateTime modifiedOn, string firstName, string lastName, string emailAddress, ICollection<DomainEvent> domainEvents, ICollection<IPublishNotification> integrationEvents) 
            : base(id, uid, suid, deletedOn, modifiedOn, domainEvents, integrationEvents)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string EmailAddress { get; init; }


    }
}
