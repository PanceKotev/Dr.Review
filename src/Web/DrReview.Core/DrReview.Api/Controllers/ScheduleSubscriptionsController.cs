namespace DrReview.Api.Controllers
{
    using System.Net;
    using DrReview.Common.Dtos.Doctor;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Query;
    using DrReview.Common.Results;
    using DrReview.Contracts.Filters.Enums;
    using DrReview.Contracts.Filters;
    using DrReview.Contracts.Requests;
    using DrReview.Modules.ScheduleNotifications.Application.Commands;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;
    using DrReview.Contracts.Dtos;
    using DrReview.Modules.ScheduleNotifications.Application.Queries;

    [Route("api/v1/schedules")]
    public class ScheduleSubscriptionsController : BaseController
    {
        private readonly IDrReviewMediatorService _mediator;

        public ScheduleSubscriptionsController(IDrReviewMediatorService mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        [RequiredScope(new[] { "drreview.read", "drreview.write" })]
        [ProducesResponseType(typeof(Result<EmptyValue>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SubscribeToDoctorScheduleAsync([FromBody] SubscribeToDoctorsScheduleRequest request)
        {
            return OkOrError(await _mediator.SendAsync(new SubscribeToScheduleCommand(
                                                                               doctorSuid: request.DoctorSuid,
                                                                               rangeFrom: request.RangeFrom,
                                                                               rangeTo: request.RangeTo)));
        }

        [HttpPut]
        [Authorize]
        [RequiredScope(new[] { "drreview.read", "drreview.write" })]
        [ProducesResponseType(typeof(Result<EmptyValue>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateSubscriptionRangeAsync([FromBody] UpdateSubscriptionRangeRequest request)
        {
            return OkOrError(await _mediator.SendAsync(new UpdateRangeScheduleCommand(
                                                                               scheduleSuid: request.ScheduleSuid,
                                                                               rangeFrom: request.RangeFrom,
                                                                               rangeTo: request.RangeTo)));
        }

        [HttpDelete]
        [Route("/{scheduleSuid}")]
        [Authorize]
        [RequiredScope(new[] { "drreview.read", "drreview.write" })]
        [ProducesResponseType(typeof(Result<EmptyValue>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UnsubscribeFromDoctorsScheduleAsync([FromRoute] string scheduleSuid)
        {
            return OkOrError(await _mediator.SendAsync(new UnsubscribeFromScheduleCommand(scheduleSuid: scheduleSuid)));
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(Result<GetScheduleSubscriptionsDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetScheduleSubscriptionsAsync([FromQuery] FilterBy? filterBy, [FromQuery] string? filterByValue, [FromQuery] int page = 0, [FromQuery] int itemsCount = 10000)
        {
            FilterByValue? filter = filterBy != null && !string.IsNullOrEmpty(filterByValue) ? new FilterByValue(filterBy ?? FilterBy.ALL, filterByValue) : null;

            GetScheduleSubscriptionsQuery query = new GetScheduleSubscriptionsQuery(new GetScheduleSubscriptionsFilter(page, itemsCount, string.Empty, filter));

            return OkOrError(await _mediator.SendAsync(query));
        }
    }
}
