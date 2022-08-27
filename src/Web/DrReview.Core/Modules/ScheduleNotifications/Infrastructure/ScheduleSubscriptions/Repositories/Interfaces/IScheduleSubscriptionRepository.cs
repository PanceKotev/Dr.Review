namespace DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Repositories.Interfaces
{
    public interface IScheduleSubscriptionRepository
    {
        public void InsertScheduleSubscription(Entities.ScheduleSubscription scheduleSubscription);

        public void UpdateScheduleSubscription(Entities.ScheduleSubscription scheduleSubscription);
    }
}
