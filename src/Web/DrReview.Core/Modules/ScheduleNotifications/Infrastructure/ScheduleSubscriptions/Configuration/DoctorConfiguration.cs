namespace DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Configuration
{
    using DrReview.Common.Infrastructure.Configurations;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DoctorConfiguration : BaseEntityConfiguration<Doctor>
    {
        public DoctorConfiguration(string schema)
        : base(schema)
        {
        }

        public override void Configure(EntityTypeBuilder<Doctor> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FirstName).HasMaxLength(500).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(500).IsRequired();

            builder.ToTable("Doctor", Schema);
        }
    }
}
