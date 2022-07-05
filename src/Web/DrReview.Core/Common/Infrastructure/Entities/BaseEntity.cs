#nullable disable
namespace DrReview.Common.Infrastructure.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using DrReview.Common.Mediator.Contracts;

    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            DomainEvents = new List<DomainEvent>();
        }

        protected BaseEntity(long id, Guid uid, string suid, DateTime? deletedOn, DateTime modifiedOn)
        {
            Id = id;
            Uid = uid;
            Suid = suid;
            DeletedOn = deletedOn;
            ModifiedOn = modifiedOn;
            DomainEvents = new ();
        }

        public long Id { get; init; }

        public Guid Uid { get; init; }

        public string Suid { get; init; }

        public DateTime? DeletedOn { get; init; }

        public DateTime ModifiedOn { get; set; }

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; init; }

        public void AddDomainEvent(DomainEvent domainEvent)
        {
            if (domainEvent is null)
            {
                throw new ArgumentNullException(nameof(domainEvent), "Domain event cannot be null");
            }

            DomainEvents.Add(domainEvent);
        }

        public void AddDomainEvents(IReadOnlyList<DomainEvent> domainEvents)
        {
            if (domainEvents is null)
            {
                throw new ArgumentNullException(nameof(domainEvents), "Domain events cannot be null");
            }

            DomainEvents.AddRange(domainEvents);
        }

        public void ClearDomainEvents()
        {
            DomainEvents.Clear();
        }
    }
}
