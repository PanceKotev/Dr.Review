namespace DrReview.Api.Controllers
{
    using System.Net;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Dtos.Doctor;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Query;
    using DrReview.Common.Results;
    using DrReview.Contracts.Filters;
    using DrReview.Contracts.Filters.Enums;
    using Microsoft.AspNetCore.Mvc;

    public class DoctorsController : BaseController
    {
        private readonly IDrReviewMediatorService _mediator;

        private readonly ICurrentUser _currentUser;

        public DoctorsController(IDrReviewMediatorService mediator, ICurrentUser currentUser)
        {
            _mediator = mediator;
            _currentUser = currentUser;
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(Result<List<SearchDoctorDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SearchDoctorsAsync([FromQuery] string? searchword, [FromQuery] bool filterSchedules = false)
        {
            GetDoctorsBySearchwordQuery query = new GetDoctorsBySearchwordQuery(searchword, filterSchedules);

            return OkOrError(await _mediator.SendAsync(query));
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(Result<GetDoctorsDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDoctorsAsync(
            [FromQuery] FilterBy? filterBy,
            [FromQuery] string? filterByValue,
            [FromQuery] int page = 0,
            [FromQuery] int itemsCount = 10000,
            [FromQuery] bool withSubscriptions = false)
        {
            FilterByValue? filter = filterBy != null && !string.IsNullOrEmpty(filterByValue) ? new FilterByValue(filterBy ?? FilterBy.LOCATION, filterByValue) : null;

            GetDoctorsQuery query = new GetDoctorsQuery(new GetDoctorsFilter(page, itemsCount, string.Empty, filter), withSubscriptions);

            return OkOrError(await _mediator.SendAsync(query));
        }

        [HttpGet]
        [Route("count")]
        [ProducesResponseType(typeof(Result<long>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDoctorsCountAsync()
        {
            GetDoctorsCountQuery query = new GetDoctorsCountQuery();

            return OkOrError(await _mediator.SendAsync(query));
        }

        [HttpGet]
        [Route("{doctorSuid}")]
        [ProducesResponseType(typeof(Result<GetDoctorDetailsDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDoctorAsync([FromRoute] string doctorSuid)
        {
            GetDoctorDetailsQuery query = new GetDoctorDetailsQuery(doctorSuid);

            return OkOrError(await _mediator.SendAsync(query));
        }
    }
}
