namespace DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Configuration
{
    using DrReview.Common.Infrastructure.Configurations;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ScheduleSubscriberConfiguration : BaseEntityConfiguration<ScheduleSubscriber>
    {
        public ScheduleSubscriberConfiguration(string schema)
            : base(schema)
        {
        }

        public override void Configure(EntityTypeBuilder<ScheduleSubscriber> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FirstName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Email).HasColumnName("EmailAddress").HasMaxLength(200).IsRequired(false);

            builder.ToTable("User", Schema);
        }
    }
}
