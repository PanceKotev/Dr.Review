﻿namespace DrReview.Api.Controllers
{
    using System.Net;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Results;
    using DrReview.Contracts.Dtos;
    using DrReview.Contracts.Requests;
    using DrReview.Modules.Review.Application.Commands;
    using DrReview.Modules.Review.Application.Queries;
    using DrReview.Modules.Review.Infrastructure.Common.UnitOfWork.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;

    [Route("api/v1/reviews")]
    public class ReviewController : BaseController
    {

        private readonly IDrReviewMediatorService _mediator;

        public ReviewController(IDrReviewMediatorService mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        [RequiredScope("drreview.read")]
        [ProducesResponseType(typeof(Result<List<GetReviewsDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReviewsAsync(
                                                                [FromQuery] string? reviewerSuid,
                                                                [FromQuery] string? revieweeSuid,
                                                                [FromQuery] int startPage = 0,
                                                                [FromQuery] int itemsPerPage = 50)
        {
            return OkOrError(await _mediator.SendAsync(new GetReviewsQuery(
                                                                           startPage: startPage,
                                                                           itemsPerPage: itemsPerPage,
                                                                           revieweeSuid: revieweeSuid,
                                                                           reviewerSuid: reviewerSuid)));
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

        [HttpGet]
        [Authorize]
        [RequiredScope(new[] { "drreview.read", "drreview.write" })]
        [Route("{revieweeSuid}/summary")]
        [ProducesResponseType(typeof(Result<GetReviewSummaryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReviewSummaryAsync([FromRoute] string revieweeSuid)
        {
            return OkOrError(await _mediator.SendAsync(new GetReviewSummaryQuery(revieweeSuid: revieweeSuid)));
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

        [HttpPost]
        [Authorize]
        [Route("vote")]
        [RequiredScope(new[] { "drreview.read", "drreview.write" })]
        [ProducesResponseType(typeof(Result<EmptyValue>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> VoteOnReviewAsync([FromBody] VoteOnReviewRequest request)
        {
            return OkOrError(await _mediator.SendAsync(new VoteOnReviewCommand(
                                                                               reviewSuid: request.ReviewSuid,
                                                                               vote: request.Vote)));
        }
    }
}
