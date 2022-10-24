namespace DrReview.Modules.ScheduleNotifications.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.Contexts;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.UnitOfWork.Interfaces;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using Microsoft.EntityFrameworkCore;

    public class UnsubscribeFromSchedulesCommand : ICommand<Result<EmptyValue>>
    {
        public UnsubscribeFromSchedulesCommand(List<string> scheduleSuids)
        {
            ScheduleSuids = scheduleSuids;
        }

        public List<string> ScheduleSuids { get; init; }
    }

    public class UnsubscribeToSchedulesCommandHandler : ICommandHandler<UnsubscribeFromSchedulesCommand, Result<EmptyValue>>
    {
        private readonly ICurrentUser _currentUser;

        private readonly IScheduleNotificationsUnitOfWork _unitOfWork;

        private readonly ScheduleNotificationReadonlyDatabaseContext _database;

        public UnsubscribeToSchedulesCommandHandler(
            ICurrentUser currentUser,
            IScheduleNotificationsUnitOfWork unitOfWork,
            ScheduleNotificationReadonlyDatabaseContext database)
        {
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _database = database;
        }

        public async Task<Result<EmptyValue>> Handle(UnsubscribeFromSchedulesCommand request, CancellationToken cancellationToken)
        {
            List<ScheduleSubscription> existingSubscriptions = request.ScheduleSuids.Any() ?
                await _database.ScheduleSubscriptions
                                .Where(x => request.ScheduleSuids.Contains(x.Suid))
                                .ToListAsync() :
                new List<ScheduleSubscription>();

            if (!existingSubscriptions.Any())
            {
                return Result.Invalid<EmptyValue>(ResultCodes.ScheduleSubscriptionsNotFound);
            }

            existingSubscriptions.ForEach(x => x.Delete());

            _unitOfWork.ScheduleSubscriptions.UpdateScheduleSubscriptions(existingSubscriptions);

            await _unitOfWork.SaveAsync();

            return Result.Ok(EmptyValue.Value);
        }
    }
}
