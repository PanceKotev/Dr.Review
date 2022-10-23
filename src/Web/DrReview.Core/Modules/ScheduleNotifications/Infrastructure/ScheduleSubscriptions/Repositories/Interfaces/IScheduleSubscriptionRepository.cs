namespace DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Repositories.Interfaces
{
    public interface IScheduleSubscriptionRepository
    {
        public void InsertScheduleSubscription(Entities.ScheduleSubscription scheduleSubscription);

        public void InsertScheduleSubscriptions(List<Entities.ScheduleSubscription> scheduleSubscriptions);

        public void UpdateScheduleSubscription(Entities.ScheduleSubscription scheduleSubscription);
    }
}
