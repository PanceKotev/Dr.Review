namespace DrReview.Modules.Review.Infrastructure.Review.Configuration
{
    using DrReview.Common.Infrastructure.Configurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class VoteConfiguration : BaseEntityConfiguration<Entities.Vote>
    {
        public VoteConfiguration(string schema)
            : base(schema)
        {
        }

        public override void Configure(EntityTypeBuilder<Entities.Vote> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Upvoted);

            builder.HasOne(x => x.Review).WithOne().HasForeignKey<Entities.Vote>(v => v.ReviewFK);
            builder.HasOne(x => x.Reviewer).WithOne().HasForeignKey<Entities.Vote>(v => v.ReviewerFK);

            builder.ToTable("Vote", Schema);
        }
    }
}
