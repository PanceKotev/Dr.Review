namespace DrReview.Api.Controllers
{
    using System.Net;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Query;
    using DrReview.Common.Results;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;

    [Route("api/[controller]")]
    [RequiredScope("drreview.read")]
    public class InstitutionController : BaseController
    {
        private readonly IDrReviewMediatorService _mediatorService;

        public InstitutionController(
            IDrReviewMediatorService mediatorService)
        {
            _mediatorService = mediatorService;
        }

        [HttpGet]
        [Route("options")]
        [ProducesResponseType(typeof(Result<List<string>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetInstitutionOptionsAsync()
        {
            return OkOrError(await _mediatorService.SendAsync(new GetInstitutionOptionsQuery()));
        }
    }
}
