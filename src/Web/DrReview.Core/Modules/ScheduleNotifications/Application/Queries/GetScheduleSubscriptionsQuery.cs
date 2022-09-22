namespace DrReview.Modules.ScheduleNotifications.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Contracts.Dtos;
    using DrReview.Contracts.Filters;
    using DrReview.Contracts.Filters.Enums;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.Common.Contexts;
    using DrReview.Modules.ScheduleNotifications.Infrastructure.ScheduleSubscriptions.Entities;
    using LinqKit;
    using Microsoft.EntityFrameworkCore;

    public class GetScheduleSubscriptionsQuery : IQuery<Result<GetScheduleSubscriptionsDto>>
    {
        public GetScheduleSubscriptionsQuery(GetScheduleSubscriptionsFilter? filter)
        {
            Filter = filter;
        }

        public GetScheduleSubscriptionsFilter? Filter { get; init; }
    }

    public class GetScheduleSubscriptionsQueryHandler : IQueryHandler<GetScheduleSubscriptionsQuery, Result<GetScheduleSubscriptionsDto>>
    {
        private readonly ScheduleNotificationReadonlyDatabaseContext _databaseContext;

        private readonly ICurrentUser _currentUser;

        public GetScheduleSubscriptionsQueryHandler(
            ScheduleNotificationReadonlyDatabaseContext databaseContext,
            ICurrentUser currentUser)
        {
            _databaseContext = databaseContext;
            _currentUser = currentUser;
        }

        public async Task<Result<GetScheduleSubscriptionsDto>> Handle(GetScheduleSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            ExpressionStarter<ScheduleSubscription> predicate = PredicateBuilder.New<ScheduleSubscription>(true);

            if (request.Filter?.FilterBy is not null)
            {
                switch (request.Filter.FilterBy.Property)
                {
                    case FilterBy.MINE:
                        {
                            predicate.And(s => s.ScheduleSubscriber!.Uid == _currentUser.Uid);
                            break;
                        }
                }
            }

            long totalCount = await _databaseContext.ScheduleSubscriptions.CountAsync();

            List<GetScheduleSubscriptionDto> results = await _databaseContext.ScheduleSubscriptions
                                                .Include(x => x.ScheduleSubscriber)
                                                .Where(predicate)
                                                .Skip(request.Filter is not null ? request.Filter.StartPage * request.Filter.ItemsPerPage : 0)
                                                .Take(request.Filter is not null ? request.Filter.ItemsPerPage : 10000)
                                                .Select(x => new GetScheduleSubscriptionDto(
                                                    x.Suid,
                                                    new ScheduleSubscriptionRangeDto(x.RangeFrom, x.RangeTo)))
                                                .ToListAsync();

            GetScheduleSubscriptionsDto result = new GetScheduleSubscriptionsDto(
                                totalCount: totalCount,
                                subscriptions: results);

            return Result.Ok(result);
        }
    }
}
