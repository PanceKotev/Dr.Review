#nullable disable
namespace DrReview.Contracts.Requests
{
    public class CreateReviewRequest
    {
        public string RevieweeSuid { get; set; }

        public string Comment { get; set; }

        public decimal Score { get; set; }

        public bool Anonymous { get; set; }
    }
}
