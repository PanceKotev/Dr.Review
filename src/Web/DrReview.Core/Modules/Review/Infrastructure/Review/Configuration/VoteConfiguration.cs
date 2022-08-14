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

            builder.ToTable("Vote", Schema);
        }
    }
}
