#nullable disable
namespace DrReview.Modules.ScheduleNotifications.Infrastructure.Common.UnitOfWork
{
    using DrReview.Common.Infrastructure.UnitOfWork;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.Contexts;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.UnitOfWork.Interfaces;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Repositories;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Repositories.Interfaces;

    public class ScheduleNotificationsUnitOfWork : BaseUnitOfWork, IScheduleNotificationsUnitOfWork
    {
        private IScheduleSubscriptionRepository _scheduleSubscriptions;

        public ScheduleNotificationsUnitOfWork(
                                              ScheduleNotificationDatabaseContext databaseContext,
                                              IDrReviewMediatorService mediatorService)
            : base(databaseContext, mediatorService)
        {
        }

        public IScheduleSubscriptionRepository ScheduleSubscriptions
        {
            get
            {
                if (_scheduleSubscriptions is null)
                {
                    _scheduleSubscriptions = new ScheduleSubscriptionRepository(DatabaseContext);
                }

                return _scheduleSubscriptions;
            }
        }
    }
}
