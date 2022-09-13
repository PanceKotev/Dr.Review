namespace DrReview.Contracts.Filters
{
    public class GetDoctorsFilter : BaseFilter
    {
        public GetDoctorsFilter(int pageStart, int itemsPerPage, string? searchWord, FilterByValue? filterBy = null)
            : base(pageStart, itemsPerPage)
        {
            FilterBy = filterBy;
            SearchWord = searchWord;
        }

        public FilterByValue? FilterBy { get; }

        public string? SearchWord { get; }
    }
}
