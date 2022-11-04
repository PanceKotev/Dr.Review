namespace DrReview.Modules.ScheduleNotifications.Application.Commands
{
    using System.Collections.Generic;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.Contexts;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.UnitOfWork.Interfaces;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using Microsoft.EntityFrameworkCore;

    public class SubscribeToMultipleSchedulesCommand : ICommand<Result<EmptyValue>>
    {
        public SubscribeToMultipleSchedulesCommand(List<string> doctorSuids, DateOnly rangeFrom, DateOnly rangeTo)
        {
            DoctorSuids = doctorSuids;
            RangeFrom = rangeFrom;
            RangeTo = rangeTo;
        }

        public List<string> DoctorSuids { get; init; }

        public DateOnly RangeFrom { get; init; }

        public DateOnly RangeTo { get; init; }
    }

    public class SubscribeToMultipleSchedulesCommandHandler : ICommandHandler<SubscribeToMultipleSchedulesCommand, Result<EmptyValue>>
    {
        private readonly ICurrentUser _currentUser;

        private readonly IScheduleNotificationsUnitOfWork _unitOfWork;

        private readonly ScheduleNotificationReadonlyDatabaseContext _database;

        public SubscribeToMultipleSchedulesCommandHandler(
            ICurrentUser currentUser,
            IScheduleNotificationsUnitOfWork unitOfWork,
            ScheduleNotificationReadonlyDatabaseContext database)
        {
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _database = database;
        }

        public async Task<Result<EmptyValue>> Handle(SubscribeToMultipleSchedulesCommand request, CancellationToken cancellationToken)
        {
            ScheduleSubscriber? user = await _database.ScheduleSubscribers.FirstOrDefaultAsync(x => x.Uid == _currentUser.Uid);

            if (user is null)
            {
                return Result.NotFound<EmptyValue>(ResultCodes.UserNotFound);
            }

            List<long> doctorFks = request.DoctorSuids.Any() ?
                await _database.Doctors.Where(x => request.DoctorSuids.Contains(x.Suid)).Select(d => d.Id).ToListAsync() :
                new List<long>();

            if (!doctorFks.Any())
            {
                return Result.NotFound<EmptyValue>(ResultCodes.DoctorNotFound);
            }

            List<long>? existingSubscriptionDoctorFks = await _database.ScheduleSubscriptions
                                                                        .Where(x => x.UserFK == user.Id && doctorFks.Contains(x.DoctorFK))
                                                                        .Select(x => x.DoctorFK)
                                                                        .ToListAsync();

            List<long> filteredDoctorFks = doctorFks.Where(x => !existingSubscriptionDoctorFks.Contains(x)).ToList();

            if (!filteredDoctorFks.Any())
            {
                return Result.Invalid<EmptyValue>(ResultCodes.ScheduleSubscriptionAlreadyExists);
            }

            List<Result<ScheduleSubscription>> createdSubscriptionsOrError =
                filteredDoctorFks.Select(doctorId => ScheduleSubscription.Create(
                                                                                 doctorFK: doctorId,
                                                                                 userFK: user.Id,
                                                                                 rangeFrom: request.RangeFrom,
                                                                                 rangeTo: request.RangeTo)).ToList();

            List<ScheduleSubscription> successfullCreations = createdSubscriptionsOrError
                .Where(x => x.IsSuccess)
                .Select(x => x.Value)
                .ToList();

            List<Result<ScheduleSubscription>> failedCreations = createdSubscriptionsOrError
                .Where(x => x.IsFailure)
                .ToList();

            if (!successfullCreations.Any())
            {
                return Result.FromError<EmptyValue>(failedCreations.First());
            }

            _unitOfWork.ScheduleSubscriptions.InsertScheduleSubscriptions(successfullCreations);

            await _unitOfWork.SaveAsync();

            return Result.Ok(EmptyValue.Value);
        }
    }
}
