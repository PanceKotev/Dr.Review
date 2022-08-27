namespace DrReview.Modules.ScheduleNotifications.Infrastructure.Common.UnitOfWork.Interfaces
{
    using System.Threading.Tasks;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Repositories.Interfaces;

    public interface IScheduleNotificationsUnitOfWork
    {
        public IScheduleSubscriptionRepository ScheduleSubscriptions { get; }

        public Task SaveAsync();
    }
}
