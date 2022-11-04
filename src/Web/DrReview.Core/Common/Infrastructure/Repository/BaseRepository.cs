namespace DrReview.Common.Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using DrReview.Common.Infrastructure.Entities;
    using Microsoft.EntityFrameworkCore;

    public class BaseRepository<TAggregate>
        where TAggregate : AggregateRoot
    {

        private readonly DbContext _databaseContext;

        public BaseRepository(DbContext dbContext)
        {
            _databaseContext = dbContext;
            DbSet = dbContext.Set<TAggregate>();
        }

        private DbSet<TAggregate> DbSet { get; }

        public IQueryable<TAggregate> IgnoreQueryFilters()
        {
            return DbSet.IgnoreQueryFilters();
        }

        public IQueryable<TAggregate> Query()
        {
            return DbSet;
        }

        public IQueryable<TBaseEntity> QueryNoTrackingOf<TBaseEntity>()
            where TBaseEntity : BaseEntity
        {
            return _databaseContext.Set<TBaseEntity>();
        }

        public IQueryable<TBaseEntity> QueryOf<TBaseEntity>()
            where TBaseEntity : BaseEntity
        {
            return _databaseContext.Set<TBaseEntity>().AsNoTracking();
        }

        public IQueryable<TAggregate> QueryNoTracking()
        {
            return DbSet.AsNoTracking();
        }

        public void Insert(TAggregate entity)
        {
            DbSet.Add(entity);
        }

        public void InsertOf<TEntity>(TEntity entity)
            where TEntity : BaseEntity
        {
            _databaseContext.Set<TEntity>().Add(entity);
        }

        public void InsertRangeOf<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : BaseEntity
        {
            _databaseContext.Set<TEntity>().AddRange(entities);
        }

        public void InsertRange(IEnumerable<TAggregate> entities)
        {
            DbSet.AddRange(entities);
        }

        public void AttachOrUpdate<TEntity>(TEntity entity, EntityState entityState)
        where TEntity : BaseEntity
        {
            TEntity? existingEntity = _databaseContext.Set<TEntity>().Local.SingleOrDefault(e => e.Id == entity.Id);

            if (existingEntity is null)
            {
                _databaseContext.Entry(entity).State = entityState;
            }
            else
            {
                _databaseContext.Entry(existingEntity).CurrentValues.SetValues(entity);
                existingEntity.AddDomainEvents(entity.DomainEvents);
                _databaseContext.Entry(entity).State = EntityState.Detached;
            }
        }

        public void AttachOrUpdate(TAggregate entity, EntityState entityState)
        {
            TAggregate? existingEntity = _databaseContext.Set<TAggregate>().Local.SingleOrDefault(e => e.Id == entity.Id);

            if (existingEntity is null)
            {
                _databaseContext.Entry(entity).State = entityState;
            }
            else
            {
                _databaseContext.Entry(existingEntity).CurrentValues.SetValues(entity);
                existingEntity.AddDomainEvents(entity.DomainEvents);
                existingEntity.AddIntegrationEvents(entity.IntegrationEvents);
                _databaseContext.Entry(entity).State = EntityState.Detached;
            }
        }
    }
}
