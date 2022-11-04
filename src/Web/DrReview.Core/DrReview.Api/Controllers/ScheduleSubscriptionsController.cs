namespace DrReview.Api.Controllers
{
    using System.Net;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Results;
    using DrReview.Contracts.Dtos;
    using DrReview.Contracts.Filters;
    using DrReview.Contracts.Requests;
    using DrReview.Modules.ScheduleNotifications.Application.Commands;
    using DrReview.Modules.ScheduleNotifications.Application.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;

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
        public async Task<IActionResult> SubscribeToMultipleDoctorSchedulesAsync([FromBody] SubscribeToMultipleDoctorsSchedulesRequest request)
        {
            return OkOrError(await _mediator.SendAsync(new SubscribeToMultipleSchedulesCommand(
                                                                               doctorSuids: request.DoctorSuids,
                                                                               rangeFrom: request.RangeFrom,
                                                                               rangeTo: request.RangeTo)));
        }

        [HttpPut]
        [Authorize]
        [RequiredScope(new[] { "drreview.read", "drreview.write" })]
        [ProducesResponseType(typeof(Result<EmptyValue>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateSubscriptionsRangeAsync([FromBody] UpdateSubscriptionsRangeRequest request)
        {
            return OkOrError(await _mediator.SendAsync(new UpdateSubscriptionsRangeScheduleCommand(
                                                                               scheduleSuids: request.ScheduleSuids,
                                                                               rangeFrom: request.RangeFrom,
                                                                               rangeTo: request.RangeTo)));
        }

        [HttpDelete]
        [Authorize]
        [RequiredScope(new[] { "drreview.read", "drreview.write" })]
        [ProducesResponseType(typeof(Result<EmptyValue>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UnsubscribeFromMultipleDoctorsSchedulesAsync([FromBody] UnsubscribeFromMultipleDoctorSchedulesRequest unsubscribeRequest)
        {
            return OkOrError(await _mediator.SendAsync(new UnsubscribeFromSchedulesCommand(scheduleSuids: unsubscribeRequest.ScheduleSuids)));
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(Result<GetScheduleSubscriptionsDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetScheduleSubscriptionsAsync(
            [FromQuery] DateOnly? rangeFrom,
            [FromQuery] DateOnly? rangeTo,
            [FromQuery] int page = 0,
            [FromQuery] int itemsPerPage = 50)
        {
            GetScheduleSubscriptionsQuery query = new GetScheduleSubscriptionsQuery(new GetScheduleSubscriptionsFilter(page, itemsPerPage, rangeFrom, rangeTo));

            return OkOrError(await _mediator.SendAsync(query));
        }
    }
}
