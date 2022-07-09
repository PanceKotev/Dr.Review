namespace DrReview.Contracts.Dtos
{
    using System;

    public class GetReviewsDto
    {
        public GetReviewsDto(string suid, string reviewerName, string revieweeName, DateTime lastUpdatedOn, string? comment, decimal score)
        {
            Suid = suid;
            ReviewerName = reviewerName;
            RevieweeName = revieweeName;
            LastUpdatedOn = lastUpdatedOn;
            Comment = comment;
            Score = score;
        }

        public string Suid { get; init; }

        public string ReviewerName { get; init; }

        public string RevieweeName { get; init; }

        public DateTime LastUpdatedOn { get; init; }

        public string? Comment { get; init; }

        public decimal Score { get; init; }
    }
}
