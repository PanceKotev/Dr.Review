﻿namespace DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Configuration
{
    using DrReview.Common.Infrastructure.Configurations;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ScheduleSubscriptionConfiguration : BaseEntityConfiguration<ScheduleSubscription>
    {
        public ScheduleSubscriptionConfiguration(string schema)
            : base(schema)
        {
        }

        public override void Configure(EntityTypeBuilder<ScheduleSubscription> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.UserFK);

            builder.HasOne(x => x.ScheduleSubscriber).WithOne().HasForeignKey<ScheduleSubscription>(s => s.UserFK);
            builder.HasOne(x => x.Doctor).WithOne().HasForeignKey<ScheduleSubscription>(s => s.DoctorFK);
        }
    }
}
