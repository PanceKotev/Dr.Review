namespace DrReview.Api.Controllers
{
    using System.Net;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Results;
    using DrReview.Contracts.Dtos;
    using DrReview.Contracts.Requests;
    using DrReview.Modules.User.Application.Commands.User;
    using DrReview.Modules.User.Application.Queries.User;
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

        [Route("me")]
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(Result<GetUserDetailsDto>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetUserDetailsAsync()
        {
            Result<GetUserDetailsDto> user = await _mediatorService.SendAsync(new GetUserDetailsQuery());

            return Ok(user);
        }

        [Route("update")]
        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(Result<EmptyValue>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request)
        {
            Result<EmptyValue> user = await _mediatorService.SendAsync(
                                                    new UpdateUserCommand(request.FirstName, request.LastName));

            return Ok(user);
        }
    }
}
