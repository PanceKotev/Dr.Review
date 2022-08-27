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

    public class UnsubscribeFromScheduleCommand : ICommand<Result<EmptyValue>>
    {
        public UnsubscribeFromScheduleCommand(string scheduleSuid)
        {
            ScheduleSuid = scheduleSuid;
        }

        public string ScheduleSuid { get; init; }
    }

    public class UnsubscribeToScheduleCommandHandler : ICommandHandler<UnsubscribeFromScheduleCommand, Result<EmptyValue>>
    {
        private readonly ICurrentUser _currentUser;

        private readonly IScheduleNotificationsUnitOfWork _unitOfWork;

        private readonly ScheduleNotificationReadonlyDatabaseContext _database;

        public UnsubscribeToScheduleCommandHandler(
            ICurrentUser currentUser,
            IScheduleNotificationsUnitOfWork unitOfWork,
            ScheduleNotificationReadonlyDatabaseContext database)
        {
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _database = database;
        }

        public async Task<Result<EmptyValue>> Handle(UnsubscribeFromScheduleCommand request, CancellationToken cancellationToken)
        {
            ScheduleSubscription? existingSubscription = await _database.ScheduleSubscriptions
                                                                        .FirstOrDefaultAsync(x => x.Suid == request.ScheduleSuid);

            if (existingSubscription is null)
            {
                return Result.Invalid<EmptyValue>(ResultCodes.ScheduleSubscriptionNotFound);
            }

            existingSubscription.Delete();

            _unitOfWork.ScheduleSubscriptions.UpdateScheduleSubscription(existingSubscription);

            await _unitOfWork.SaveAsync();

            return Result.Ok(EmptyValue.Value);
        }
    }
}
