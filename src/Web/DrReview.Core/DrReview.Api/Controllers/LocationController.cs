namespace DrReview.Api.Controllers
{
    using System.Net;
    using DrReview.Common.Dtos.Doctor;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Query;
    using DrReview.Common.Results;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;

    [Route("api/[controller]")]
    [RequiredScope("drreview.read")]
    public class LocationController : BaseController
    {
        private readonly IDrReviewMediatorService _mediatorService;

        public LocationController(
            IDrReviewMediatorService mediatorService)
        {
            _mediatorService = mediatorService;
        }

        [HttpGet]
        [Route("options")]
        [ProducesResponseType(typeof(Result<List<GetLocationOptionsDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLocationsAsync()
        {
            return OkOrError(await _mediatorService.SendAsync(new GetLocationOptionsQuery()));
        }
    }
}
