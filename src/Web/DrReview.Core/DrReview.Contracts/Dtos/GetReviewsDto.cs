namespace DrReview.Contracts.Dtos
{
    using System;

    public class GetReviewsDto
    {
        public GetReviewsDto(List<GetReviewDto> reviews, GetReviewDto? currentUserReview)
        {
            Reviews = reviews;
            CurrentUserReview = currentUserReview;
        }

        public List<GetReviewDto> Reviews { get; init; }

        public GetReviewDto? CurrentUserReview { get; init; } = null;
    }

    public class GetReviewDto
    {
        public GetReviewDto(string suid, string reviewerName, string revieweeName, DateTime lastUpdatedOn, string? comment, decimal score, long upvotes, long downvotes)
        {
            Suid = suid;
            ReviewerName = reviewerName;
            RevieweeName = revieweeName;
            LastUpdatedOn = lastUpdatedOn;
            Comment = comment;
            Score = score;
            Upvotes = upvotes;
            Downvotes = downvotes;
        }

        public string Suid { get; init; }

        public string ReviewerName { get; init; }

        public string RevieweeName { get; init; }

        public DateTime LastUpdatedOn { get; init; }

        public string? Comment { get; init; }

        public decimal Score { get; init; }

        public long Upvotes { get; init; }

        public long Downvotes { get; init; }
    }
}
