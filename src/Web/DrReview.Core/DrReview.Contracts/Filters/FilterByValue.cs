namespace DrReview.Contracts.Filters
{
    using DrReview.Contracts.Filters.Enums;

    public class FilterByValue
    {
        public FilterByValue(FilterBy property, string value)
        {
            Property = property;
            Value = value;
        }

        public FilterBy Property { get; set; }

        public string Value { get; set; }
    }
}
