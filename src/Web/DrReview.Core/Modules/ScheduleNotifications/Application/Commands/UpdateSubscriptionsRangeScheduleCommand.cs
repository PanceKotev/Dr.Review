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

    public class UpdateSubscriptionsRangeScheduleCommand : ICommand<Result<EmptyValue>>
    {
        public UpdateSubscriptionsRangeScheduleCommand(List<string> scheduleSuids, DateOnly rangeFrom, DateOnly rangeTo)
        {
            ScheduleSuids = scheduleSuids;
            RangeFrom = rangeFrom;
            RangeTo = rangeTo;
        }

        public List<string> ScheduleSuids { get; init; }

        public DateOnly RangeFrom { get; init; }

        public DateOnly RangeTo { get; init; }
    }

    public class UpdateSubscriptionsRangeScheduleCommandHandler : ICommandHandler<UpdateSubscriptionsRangeScheduleCommand, Result<EmptyValue>>
    {
        private readonly ICurrentUser _currentUser;

        private readonly IScheduleNotificationsUnitOfWork _unitOfWork;

        private readonly ScheduleNotificationReadonlyDatabaseContext _database;

        public UpdateSubscriptionsRangeScheduleCommandHandler(
            ICurrentUser currentUser,
            IScheduleNotificationsUnitOfWork unitOfWork,
            ScheduleNotificationReadonlyDatabaseContext database)
        {
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _database = database;
        }

        public async Task<Result<EmptyValue>> Handle(UpdateSubscriptionsRangeScheduleCommand request, CancellationToken cancellationToken)
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

            List<Result<ScheduleSubscription>> updateSubscriptionsOrError = existingSubscriptions.Select(

                existingSubscription => existingSubscription.Update(
                                                                rangeFrom: request.RangeFrom,
                                                                rangeTo: request.RangeTo)).ToList();

            List<ScheduleSubscription> successfullyUpdated = updateSubscriptionsOrError
                                                                .Where(x => x.IsSuccess)
                                                                .Select(x => x.Value)
                                                                .ToList();
            List<Result<ScheduleSubscription>> failedUpdate = updateSubscriptionsOrError
                                                          .Where(x => x.IsFailure)
                                                          .ToList();

            if (!successfullyUpdated.Any())
            {
                return Result.FromError<EmptyValue>(failedUpdate.First());
            }

            _unitOfWork.ScheduleSubscriptions.UpdateScheduleSubscriptions(successfullyUpdated);

            await _unitOfWork.SaveAsync();

            return Result.Ok(EmptyValue.Value);
        }
    }
}
