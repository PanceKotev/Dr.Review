namespace DrReview.Api.Controllers
{
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Modules.User.Application.Commands.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;

    [Route("api/v1/users")]
    [RequiredScope("drreview.read")]
    public class UserController : BaseController
    {
        private readonly IDrReviewMediatorService _mediatorService;

        private readonly ICurrentUser _currentUser;

        public UserController(IDrReviewMediatorService mediatorService, ICurrentUser currentUser)
        {
            _mediatorService = mediatorService;
            _currentUser = currentUser;
        }

        [Route("create")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUserAsync()
        {
            await _mediatorService.SendAsync(new CreateUserIfNotExistsCommand());

            return Ok();
        }
    }
}
