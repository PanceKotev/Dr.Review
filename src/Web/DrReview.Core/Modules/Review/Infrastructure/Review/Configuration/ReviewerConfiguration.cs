namespace DrReview.Modules.Review.Infrastructure.Review.Configuration
{
    using DrReview.Common.Infrastructure.Configurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReviewerConfiguration : BaseEntityConfiguration<Entities.Reviewer>
    {
        public ReviewerConfiguration(string schema)
            : base(schema)
        {
        }

        public override void Configure(EntityTypeBuilder<Entities.Reviewer> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FirstName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Email).HasColumnName("EmailAddress").HasMaxLength(200).IsRequired(false);

            builder.ToTable("User", Schema);
        }
    }
}
