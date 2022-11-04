namespace DrReview.Contracts.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GetScheduleSubscriptionsFilter : BaseFilter
    {
        public GetScheduleSubscriptionsFilter(int pageStart, int itemsPerPage, DateOnly? rangeFrom, DateOnly? rangeTo)
            : base(pageStart, itemsPerPage)
        {
            RangeFrom = rangeFrom;
            RangeTo = rangeTo;
        }

        public DateOnly? RangeFrom { get; init; }

        public DateOnly? RangeTo { get; init; }
    }
}
