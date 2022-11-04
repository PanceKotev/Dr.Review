namespace DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Repositories.Interfaces
{
    public interface IScheduleSubscriptionRepository
    {
        public void InsertScheduleSubscriptions(List<Entities.ScheduleSubscription> scheduleSubscriptions);

        public void UpdateScheduleSubscriptions(List<Entities.ScheduleSubscription> scheduleSubscriptions);

    }
}
