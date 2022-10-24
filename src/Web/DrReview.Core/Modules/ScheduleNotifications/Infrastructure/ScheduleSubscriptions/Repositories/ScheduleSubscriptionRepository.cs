namespace DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Repositories
{
    using DrReview.Common.Infrastructure.Repository;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ScheduleSubscriptionRepository : BaseRepository<Entities.ScheduleSubscription>, IScheduleSubscriptionRepository
    {
        public ScheduleSubscriptionRepository(DbContext dbContext)
        : base(dbContext)
        {
        }

        public void InsertScheduleSubscriptions(List<ScheduleSubscription> scheduleSubscriptions)
        {
            InsertRange(scheduleSubscriptions);
        }

        public void UpdateScheduleSubscriptions(List<ScheduleSubscription> scheduleSubscriptions)
        {
            scheduleSubscriptions.ForEach(scheduleSubscription => AttachOrUpdate(scheduleSubscription, EntityState.Modified));
        }
    }
}
