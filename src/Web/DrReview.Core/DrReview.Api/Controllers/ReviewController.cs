﻿namespace DrReview.Api.Controllers
{
    using System.Net;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Results;
    using DrReview.Contracts.Requests;
    using DrReview.Modules.Review.Application.Commands;
    using DrReview.Modules.Review.Infrastructure.Common.UnitOfWork.Interfaces;
    using DrReview.Modules.Review.Infrastructure.Review.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;

    [Route("api/v1/reviews")]
    public class ReviewController : BaseController
    {
        private readonly IReviewUnitOfWork _reviewUnitOfWork;

        private readonly IDrReviewMediatorService _mediator;

        public ReviewController(IReviewUnitOfWork reviewUnitOfWork, IDrReviewMediatorService mediator)
        {
            _reviewUnitOfWork = reviewUnitOfWork;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{doctorId}")]
        [ProducesResponseType(typeof(Result<List<Review>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReviewsForDoctorAsync([FromRoute] long doctorId)
        {
            return Ok(await _reviewUnitOfWork.Reviews.GetAllReviewsForRevieweeAsync(doctorId));
        }

        [HttpPost]
        [Authorize]
        [RequiredScope(new[] { "drreview.read", "drreview.write" })]
        [ProducesResponseType(typeof(Result<EmptyValue>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateReviewForDoctorAsync([FromBody] CreateReviewRequest request)
        {
            return OkOrError(await _mediator.SendAsync(new CreateReviewCommand(
                                                                               revieweeSuid: request.RevieweeSuid,
                                                                               comment: request.Comment,
                                                                               score: request.Score)));
        }

        [HttpDelete]
        [Route("{reviewSuid}")]
        [Authorize]
        [RequiredScope(new[] { "drreview.read", "drreview.write" })]
        [ProducesResponseType(typeof(Result<EmptyValue>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteReviewAsync([FromRoute] string reviewSuid)
        {
            return OkOrError(await _mediator.SendAsync(new DeleteReviewCommand(reviewSuid: reviewSuid)));
        }

        [HttpPut]
        [Authorize]
        [RequiredScope(new[] { "drreview.read", "drreview.write" })]
        [ProducesResponseType(typeof(Result<EmptyValue>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateReviewAsync([FromBody] UpdateReviewRequest request)
        {
            return OkOrError(await _mediator.SendAsync(new UpdateReviewCommand(
                                                                               reviewSuid: request.ReviewSuid,
                                                                               comment: request.Comment,
                                                                               score: request.Score)));
        }
    }
}