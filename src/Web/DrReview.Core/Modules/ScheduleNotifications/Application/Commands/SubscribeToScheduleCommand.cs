namespace DrReview.Modules.ScheduleNotifications.Application.Commands
{
    using System.ComponentModel.DataAnnotations;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.Contexts;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.UnitOfWork.Interfaces;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using Microsoft.EntityFrameworkCore;

    public class SubscribeToScheduleCommand : ICommand<Result<EmptyValue>>
    {
        public SubscribeToScheduleCommand(string doctorSuid, DateOnly rangeFrom, DateOnly rangeTo)
        {
            DoctorSuid = doctorSuid;
            RangeFrom = rangeFrom;
            RangeTo = rangeTo;
        }

        public string DoctorSuid { get; init; }

        public DateOnly RangeFrom { get; init; }

        public DateOnly RangeTo { get; init; }
    }

    public class SubscribeToScheduleCommandHandler : ICommandHandler<SubscribeToScheduleCommand, Result<EmptyValue>>
    {
        private readonly ICurrentUser _currentUser;

        private readonly IScheduleNotificationsUnitOfWork _unitOfWork;

        private readonly ScheduleNotificationReadonlyDatabaseContext _database;

        public SubscribeToScheduleCommandHandler(
            ICurrentUser currentUser,
            IScheduleNotificationsUnitOfWork unitOfWork,
            ScheduleNotificationReadonlyDatabaseContext database)
        {
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _database = database;
        }

        public async Task<Result<EmptyValue>> Handle(SubscribeToScheduleCommand request, CancellationToken cancellationToken)
        {
            ScheduleSubscriber? user = await _database.ScheduleSubscribers.FirstOrDefaultAsync(x => x.Uid == _currentUser.Uid);

            if (user is null)
            {
                return Result.NotFound<EmptyValue>(ResultCodes.UserNotFound);
            }

            Doctor? doctor = await _database.Doctors.FirstOrDefaultAsync(x => x.Suid == request.DoctorSuid);

            if (doctor is null)
            {
                return Result.NotFound<EmptyValue>(ResultCodes.DoctorNotFound);
            }

            ScheduleSubscription? existingSubscription = await _database.ScheduleSubscriptions
                                                                        .FirstOrDefaultAsync(x => x.DoctorFK == doctor.Id && x.UserFK == user.Id);

            if (existingSubscription is not null)
            {
                return Result.Invalid<EmptyValue>(ResultCodes.ScheduleSubscriptionAlreadyExists);
            }

            Result<ScheduleSubscription> createdSubscriptionOrError = ScheduleSubscription.Create(
                                                                                    doctorFK: doctor.Id,
                                                                                    userFK: user.Id,
                                                                                    rangeFrom: request.RangeFrom,
                                                                                    rangeTo: request.RangeTo);

            if (createdSubscriptionOrError.IsFailure)
            {
                return Result.FromError<EmptyValue>(createdSubscriptionOrError);
            }

            _unitOfWork.ScheduleSubscriptions.InsertScheduleSubscription(createdSubscriptionOrError);

            await _unitOfWork.SaveAsync();

            return Result.Ok(EmptyValue.Value);
        }
    }
}
