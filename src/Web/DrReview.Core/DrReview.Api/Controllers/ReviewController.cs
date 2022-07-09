namespace DrReview.Api.Controllers
{
    using System.Net;
    using DrReview.Common.Results;
    using DrReview.Modules.Review.Infrastructure.Common.UnitOfWork.Interfaces;
    using DrReview.Modules.Review.Infrastructure.Review.Entities;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/reviews")]
    public class ReviewController : BaseController
    {
        private readonly IReviewUnitOfWork _reviewUnitOfWork;

        public ReviewController(IReviewUnitOfWork reviewUnitOfWork)
        {
            _reviewUnitOfWork = reviewUnitOfWork;
        }

        [HttpGet]
        [Route("{doctorId}")]
        [ProducesResponseType(typeof(Result<List<Review>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReviewsForDoctorAsync([FromRoute] long doctorId)
        {
            return Ok(await _reviewUnitOfWork.Reviews.GetAllReviewsForRevieweeAsync(doctorId));
        }
    }
}
