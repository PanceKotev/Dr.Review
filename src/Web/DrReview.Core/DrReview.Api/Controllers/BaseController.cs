namespace DrReview.Api.Controllers
{
    using DrReview.Common.Results;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult OkOrError<T>(Result<T> result)
        {
            IActionResult? errorResponse = GetErrorResponse(result);

            return errorResponse ?? Ok(result);
        }

        protected IActionResult OkOrError(ResultCommonLogic result)
        {
            IActionResult? errorResponse = GetErrorResponse(result);

            return errorResponse ?? Ok(result);
        }

        protected IActionResult OkEmptyOrError<T>(Result<T> result)
        {
            IActionResult? errorResponse = GetErrorResponse(result);

            return errorResponse ?? Ok(Result.Ok());
        }

        private IActionResult? GetErrorResponse(ResultCommonLogic result)
        {
            if (result.IsFailure)
            {
                IActionResult errorResponse = new ObjectResult(result)
                {
                    DeclaredType = typeof(ResultCommonLogic),
                    StatusCode = (int)result.HttpStatusCode
                };

                return errorResponse;
            }

            return null;
        }
    }
}
