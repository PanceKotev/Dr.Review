namespace DrReview.Modules.ScheduleNotifications.Application.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.Contexts;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.UnitOfWork.Interfaces;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using Microsoft.EntityFrameworkCore;

    public class UpdateRangeScheduleCommand : ICommand<Result<EmptyValue>>
    {
        public UpdateRangeScheduleCommand(string scheduleSuid, DateOnly rangeFrom, DateOnly rangeTo)
        {
            ScheduleSuid = scheduleSuid;
            RangeFrom = rangeFrom;
            RangeTo = rangeTo;
        }

        public string ScheduleSuid { get; init; }

        public DateOnly RangeFrom { get; init; }

        public DateOnly RangeTo { get; init; }
    }

    public class UpdateRangeScheduleCommandHandler : ICommandHandler<UpdateRangeScheduleCommand, Result<EmptyValue>>
    {
        private readonly ICurrentUser _currentUser;

        private readonly IScheduleNotificationsUnitOfWork _unitOfWork;

        private readonly ScheduleNotificationReadonlyDatabaseContext _database;

        public UpdateRangeScheduleCommandHandler(
            ICurrentUser currentUser,
            IScheduleNotificationsUnitOfWork unitOfWork,
            ScheduleNotificationReadonlyDatabaseContext database)
        {
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _database = database;
        }

        public async Task<Result<EmptyValue>> Handle(UpdateRangeScheduleCommand request, CancellationToken cancellationToken)
        {
            ScheduleSubscription? existingSubscription = await _database.ScheduleSubscriptions
                                                                        .FirstOrDefaultAsync(x => x.Suid == request.ScheduleSuid);

            if (existingSubscription is null)
            {
                return Result.Invalid<EmptyValue>(ResultCodes.ScheduleSubscriptionNotFound);
            }

            Result<ScheduleSubscription> updateSubscriptionOrError = existingSubscription.Update(
                rangeFrom: request.RangeFrom,
                rangeTo: request.RangeTo);

            if (updateSubscriptionOrError.IsFailure)
            {
                return Result.FromError<EmptyValue>(updateSubscriptionOrError);
            }

            _unitOfWork.ScheduleSubscriptions.UpdateScheduleSubscription(updateSubscriptionOrError);

            await _unitOfWork.SaveAsync();

            return Result.Ok(EmptyValue.Value);
        }
    }
}
