namespace DrReview.Contracts.Filters
{
    public class GetReviewsFilter : BaseFilter
    {
        public GetReviewsFilter(int pageStart, int itemsPerPage, string? revieweeSuid, string? reviewerSuid)
            : base(pageStart, itemsPerPage)
        {
            RevieweeSuid = revieweeSuid;
            ReviewerSuid = reviewerSuid;
        }

        public string? RevieweeSuid { get; init; }

        public string? ReviewerSuid { get; init; }
    }
}
