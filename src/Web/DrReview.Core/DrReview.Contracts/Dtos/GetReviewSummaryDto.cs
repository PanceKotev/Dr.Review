namespace DrReview.Contracts.Dtos
{
    using System.Collections.Generic;

    public class GetReviewSummaryDto
    {
        public GetReviewSummaryDto(decimal rating, Dictionary<int, int> reviewCountByStar)
        {
            Rating = rating;
            ReviewCountByStar = reviewCountByStar;
        }

        public decimal Rating { get; init; }

        public Dictionary<int, int> ReviewCountByStar { get; init; }
    }
}
