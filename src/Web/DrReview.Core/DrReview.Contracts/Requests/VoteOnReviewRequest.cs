namespace DrReview.Contracts.Requests
{
#nullable disable

    public class VoteOnReviewRequest
    {
        public string ReviewSuid { get; set; }

        public bool? Vote { get; set; }
    }
}
