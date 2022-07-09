namespace DrReview.Modules.Review.Infrastructure.Review.Configuration
{
    using DrReview.Common.Infrastructure.Configurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RevieweeConfiguration : BaseEntityConfiguration<Entities.Reviewee>
    {
        public RevieweeConfiguration(string schema)
            : base(schema)
        {
        }

        public override void Configure(EntityTypeBuilder<Entities.Reviewee> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FirstName).HasMaxLength(500).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(500).IsRequired();
            builder.Property(x => x.ReviewScore).IsRequired();

            builder.ToTable("Doctor", Schema);
        }
    }
}
