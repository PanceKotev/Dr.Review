namespace DrReview.Api.Controllers
{
    using System.Net;
    using DrReview.Common.Dtos.Doctor;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Query;
    using DrReview.Common.Results;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;

    [Route("api/popularity")]
    [RequiredScope("drreview.read")]
    public class PopularityController : BaseController
    {
        private readonly IDrReviewMediatorService _mediatorService;

        public PopularityController(
            IDrReviewMediatorService mediatorService)
        {
            _mediatorService = mediatorService;
        }

        [HttpGet]
        [Route("doctors/location/{locationSuid}")]
        [ProducesResponseType(typeof(Result<List<GetTopDoctorsDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTop3DoctorsNearLocationAsync(
            [FromRoute] string locationSuid,
            [FromQuery] int distance = 15,
            [FromQuery] int doctorLimit = 3)
        {
            return OkOrError(await _mediatorService.SendAsync(new GetTopDoctorsNearLocationQuery(
                locationSuid: locationSuid,
                distance: distance,
                numberOfDoctors: doctorLimit)));
        }
    }
}
