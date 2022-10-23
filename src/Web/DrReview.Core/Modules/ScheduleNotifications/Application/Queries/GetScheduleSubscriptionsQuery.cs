namespace DrReview.Modules.ScheduleNotifications.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Dtos.Doctor;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Query;
    using DrReview.Common.Results;
    using DrReview.Contracts.Dtos;
    using DrReview.Contracts.Filters;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.Contexts;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using LinqKit;
    using Microsoft.EntityFrameworkCore;

    public class GetScheduleSubscriptionsQuery : IQuery<Result<GetScheduleSubscriptionsDto>>
    {
        public GetScheduleSubscriptionsQuery(GetScheduleSubscriptionsFilter filter)
        {
            Filter = filter;
        }

        public GetScheduleSubscriptionsFilter Filter { get; init; }
    }

    public class GetScheduleSubscriptionsQueryHandler : IQueryHandler<GetScheduleSubscriptionsQuery, Result<GetScheduleSubscriptionsDto>>
    {
        private readonly ScheduleNotificationReadonlyDatabaseContext _databaseContext;

        private readonly ICurrentUser _currentUser;

        private readonly IDrReviewMediatorService _mediator;

        public GetScheduleSubscriptionsQueryHandler(
            ScheduleNotificationReadonlyDatabaseContext databaseContext,
            ICurrentUser currentUser,
            IDrReviewMediatorService mediator)
        {
            _databaseContext = databaseContext;
            _currentUser = currentUser;
            _mediator = mediator;
        }

        public async Task<Result<GetScheduleSubscriptionsDto>> Handle(GetScheduleSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            ExpressionStarter<ScheduleSubscription> predicate = PredicateBuilder.New<ScheduleSubscription>(true).And(s => s.ScheduleSubscriber!.Uid == _currentUser.Uid);

            if (request.Filter.RangeFrom != null && request.Filter.RangeTo != null)
            {
                predicate.And(s => s.RangeFrom >= request.Filter.RangeFrom && s.RangeTo <= request.Filter.RangeTo);
            }

            long totalCount = await _databaseContext.ScheduleSubscriptions.CountAsync();

            List<ScheduleSubscription> results = await _databaseContext.ScheduleSubscriptions
                                                .Include(x => x.ScheduleSubscriber)
                                                .Include(x => x.Doctor)
                                                .Where(predicate)
                                                .Skip(request.Filter is not null ? request.Filter.StartPage * request.Filter.ItemsPerPage : 0)
                                                .Take(request.Filter is not null ? request.Filter.ItemsPerPage : 10000)
                                                .ToListAsync();

            List<string> doctorSuids = results.Any() ? results.Select(x => x.Doctor!.Suid).ToList() : new List<string>();

            List<GetDoctorDetailsDto> doctorDetails = doctorSuids.Any() ?
                await _mediator.SendAsync(new GetDoctorsDetailsQuery(doctorSuids)) : new List<GetDoctorDetailsDto>();

            Dictionary<string, GetScheduleSubscriptionDoctor> groupedDoctors = doctorDetails.ToDictionary(
                k => k.Suid,
                v => new GetScheduleSubscriptionDoctor(v.Suid, v.FirstName, v.LastName, v.Location, v.Institution, v.Specialization));

            List<GetScheduleSubscriptionDto> subscriptions = results.Select(s =>
            {
                GetScheduleSubscriptionDoctor? doctor = groupedDoctors.GetValueOrDefault(s.Doctor!.Suid);

                return new GetScheduleSubscriptionDto(
                    s.Suid,
                    new ScheduleSubscriptionRangeDto(s.RangeFrom, s.RangeTo, true),
                    doctor!);

            }).ToList();

            GetScheduleSubscriptionsDto result = new GetScheduleSubscriptionsDto(
                                totalCount: totalCount,
                                subscriptions: subscriptions);

            return Result.Ok(result);
        }
    }
}
