namespace DrReview.Contracts.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GetScheduleSubscriptionsFilter : BaseFilter
    {
        public GetScheduleSubscriptionsFilter(int pageStart, int itemsPerPage, string? searchWord, FilterByValue? filterBy = null)
            : base(pageStart, itemsPerPage)
        {
            FilterBy = filterBy;
            SearchWord = searchWord;
        }

        public FilterByValue? FilterBy { get; }

        public string? SearchWord { get; }
    }
}
