#nullable disable

namespace DrReview.Common.Infrastructure.Entities
{
    using System;
    using System.Collections.Generic;
    using DrReview.Common.Mediator.Contracts;

    public abstract class AggregateRoot : BaseEntity
    {
        protected AggregateRoot()
            : base()
        {
            IntegrationEvents = new List<IPublishNotification>();
        }

        protected AggregateRoot(long id, Guid uid, string suid, DateTime? deletedOn, DateTime createdOn, ICollection<DomainEvent> domainEvents, ICollection<IPublishNotification> integrationEvents)
            : base(id, uid, suid, deletedOn, createdOn, domainEvents)
        {
            IntegrationEvents = integrationEvents.ToList();
        }

        public List<IPublishNotification> IntegrationEvents { get; init; }

        public void AddIntegrationEvent(IPublishNotification integrationEvent)
        {
            if (integrationEvent is null)
            {
                throw new ArgumentNullException(nameof(integrationEvent), "Integration event cannot be null");
            }

            IntegrationEvents.Add(integrationEvent);
        }

        public void AddIntegrationEvents(IReadOnlyList<IPublishNotification> integrationEvents)
        {
            if (integrationEvents is null)
            {
                throw new ArgumentNullException(nameof(integrationEvents), "Integration events cannot be null");
            }

            IntegrationEvents.AddRange(integrationEvents);
        }

        public void ClearIntegrationEvents()
        {
            IntegrationEvents.Clear();
        }
    }
}
