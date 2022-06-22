namespace DrReview.Api.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using DrReview.Common.Dtos.Doctor;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Query;
    using DrReview.Common.Results;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class DoctorsController : BaseController
    {
        private readonly IDrReviewMediatorService _mediator;

        public DoctorsController(IDrReviewMediatorService mediator)
        {
            _mediator = mediator;
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
