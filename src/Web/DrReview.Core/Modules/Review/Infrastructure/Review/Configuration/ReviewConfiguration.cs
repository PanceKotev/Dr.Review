namespace DrReview.Modules.Review.Infrastructure.Review.Configuration
{
    using DrReview.Common.Infrastructure.Configurations;
    using DrReview.Modules.Review.Infrastructure.Review.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReviewConfiguration : BaseEntityConfiguration<Entities.Review>
    {
        public ReviewConfiguration(string schema)
            : base(schema)
        {
        }

        public override void Configure(EntityTypeBuilder<Review> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.RevieweeFK).IsRequired();
            builder.Property(x => x.ReviewerFK).IsRequired();
            builder.Property(x => x.Score).IsRequired();
            builder.Property(x => x.Comment).HasMaxLength(1000).IsRequired(false);

            builder.HasOne(x => x.Reviewee).WithMany().HasForeignKey(x => x.RevieweeFK);
            builder.HasOne(x => x.Reviewer).WithMany().HasForeignKey(x => x.ReviewerFK);

            builder.ToTable(nameof(Review));

        }
    }
}
