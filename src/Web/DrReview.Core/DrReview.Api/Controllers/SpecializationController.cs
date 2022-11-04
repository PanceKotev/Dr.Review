namespace DrReview.Api.Controllers
{
    using System.Net;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Query;
    using DrReview.Common.Results;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;

    [Route("api/[controller]")]
    public class SpecializationController : BaseController
    {
        private readonly IDrReviewMediatorService _mediatorService;

        public SpecializationController(
            IDrReviewMediatorService mediatorService)
        {
            _mediatorService = mediatorService;
        }

        [HttpGet]
        [Route("options")]
        [ProducesResponseType(typeof(Result<List<string>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSpecializationsAsync()
        {
            return OkOrError(await _mediatorService.SendAsync(new GetSpecializationOptionsQuery()));
        }
    }
}
