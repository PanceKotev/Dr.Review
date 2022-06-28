namespace DrReview.Common.Infrastructure.Configurations
{
    using DrReview.Common.Infrastructure.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        protected BaseEntityConfiguration(string schema)
        {
            Schema = schema;
        }

        protected string Schema { get; }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Uid).IsRequired();
            builder.Property(x => x.Suid).IsRequired();
            builder.Property(x => x.DeletedOn).IsRequired(false);
            builder.Property(x => x.ModifiedOn).IsRequired();

            builder.HasQueryFilter(c => c.DeletedOn == null);
        }
    }
}
