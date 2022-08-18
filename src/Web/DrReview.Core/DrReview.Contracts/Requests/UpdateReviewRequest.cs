#nullable disable
namespace DrReview.Contracts.Requests
{
    public class UpdateReviewRequest
    {
        public string ReviewSuid { get; set; }

        public string? Comment { get; set; }

        public decimal Score { get; set; }
    }
}
