namespace DrReview.Modules.User.Infrastructure.User.Configuration
{
    using DrReview.Common.Infrastructure.Configurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : BaseEntityConfiguration<Entities.User>
    {
        public UserConfiguration(string schema) : base(schema)
        {
        }

        public override void Configure(EntityTypeBuilder<Entities.User> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FirstName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.EmailAddress).HasMaxLength(200).IsRequired(false);

            builder.ToTable(nameof(Entities.User));
        }
    }
}
