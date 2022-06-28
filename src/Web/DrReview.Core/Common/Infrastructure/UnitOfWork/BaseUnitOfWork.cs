namespace DrReview.Common.Infrastructure.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DrReview.Common.Infrastructure.Entities;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Mediator.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public abstract class BaseUnitOfWork : IDisposable
    {
        protected readonly DbContext DatabaseContext;

        private readonly EntityState[] _trackedEntityStates = new[]
        {
            EntityState.Deleted,
            EntityState.Modified,
            EntityState.Added
        };

        private readonly IDrReviewMediatorService _mediatorService;

        private bool _disposedValue;

        protected BaseUnitOfWork(DbContext databaseContext, IDrReviewMediatorService mediatorService)
        {
            this.DatabaseContext = databaseContext;
            _mediatorService = mediatorService;
        }

        /// <summary>
        /// Saves the edited entites, dispatches it's domain events
        /// before saving, sets the modified properties on the event and publishes its integration events if it is an aggregate.
        /// <br/> <br/>
        /// Call this after each database modifying method to save your changes.
        /// </summary>
        /// <returns>Nothing.</returns>
        public async Task SaveAsync()
        {
            await SetModifiedPropertiesAndDispatchDomainEventsAsync();

            PublishIntegrationEvents();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    DatabaseContext.Dispose();
                }

                _disposedValue = true;
            }
        }

        /// <summary>
        /// Sets the modified properties of the entity and dispatches it's domain events before saving.
        /// </summary>
        /// <returns>Nothing.</returns>
        private async Task SetModifiedPropertiesAndDispatchDomainEventsAsync()
        {
            IEnumerable<EntityEntry<BaseEntity>> trackedEntities = DatabaseContext.ChangeTracker.Entries<BaseEntity>().ToList();

            DateTime dateNow = DateTime.UtcNow;

            bool hasChanges = DatabaseContext.ChangeTracker.HasChanges();

            foreach (EntityEntry<BaseEntity> changedEntity in trackedEntities)
            {
                if (hasChanges && _trackedEntityStates.Contains(changedEntity.State))
                {
                    changedEntity.Entity.ModifiedOn = dateNow;
                }

                foreach (IPublishNotification domainEvent in changedEntity.Entity.DomainEvents)
                {
                    domainEvent.ProcessedOn = dateNow;

                    await _mediatorService.PublishAsync(domainEvent);
                }

                changedEntity.Entity.ClearDomainEvents();
            }

            await DatabaseContext.SaveChangesAsync();
        }

        /// <summary>
        /// Publishes the integration events in the tracked entities which are aggregate roots.
        /// </summary>
        private void PublishIntegrationEvents()
        {
            IEnumerable<AggregateRoot> aggregateRoots = DatabaseContext.ChangeTracker.Entries<AggregateRoot>()
                                                                                               .Where(x => x.Entity.IntegrationEvents != null
                                                                                                        && x.Entity.IntegrationEvents.Any())
                                                                                               .Select(x => x.Entity);

            IEnumerable<IPublishNotification> allIntegrationEvents = aggregateRoots.SelectMany(x => x.IntegrationEvents);

            DateTime dateNow = DateTime.UtcNow;

            foreach (IPublishNotification integrationEvent in allIntegrationEvents)
            {
                integrationEvent.ProcessedOn = dateNow;
            }

            foreach (AggregateRoot aggregateRootEntity in aggregateRoots)
            {
                aggregateRootEntity.ClearIntegrationEvents();
            }
        }
    }
}
