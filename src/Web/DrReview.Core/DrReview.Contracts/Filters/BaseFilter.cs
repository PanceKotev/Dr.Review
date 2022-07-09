namespace DrReview.Contracts.Filters
{
    public abstract class BaseFilter
    {
        protected BaseFilter(int startPage, int itemsPerPage)
        {
            StartPage = startPage;
            ItemsPerPage = itemsPerPage;
        }

        public int StartPage { get; init; }

        public int ItemsPerPage { get; init; }
    }
}
