namespace DrReview.Contracts.Filters
{
    public class GetReviewsFilter : BaseFilter
    {
        public GetReviewsFilter(int pageStart, int itemsPerPage, string? revieweeSuid)
            : base(pageStart, itemsPerPage)
        {
            RevieweeSuid = revieweeSuid;
        }

        public string? RevieweeSuid { get; init; }
    }
}
