namespace DrReview.Api.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Dtos.Doctor;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Query;
    using DrReview.Common.Results;
    using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> SearchDoctorsAsync([FromQuery]string? searchword)
        {
            GetDoctorsBySearchwordQuery query = new GetDoctorsBySearchwordQuery(searchword, 0, 1000);

            return OkOrError(await _mediator.SendAsync(query));
        }
    }
}
