#nullable disable
namespace DrReview.Modules.ScheduleNotifications.Infrastructure.Common.Contexts
{
    using DrReview.Common.Converters;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Configuration;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using Microsoft.EntityFrameworkCore;

    public class ScheduleNotificationDatabaseContext : DbContext
    {
        private const string SchemaName = "dbo";

        public ScheduleNotificationDatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<ScheduleSubscription> ScheduleSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ScheduleSubscriberConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new DoctorConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new ScheduleSubscriptionConfiguration(SchemaName));
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            configurationBuilder.Properties<DateOnly?>()
                .HaveConversion<NullableDateOnlyConverter>()
                .HaveColumnType("date");
        }
    }
}
