namespace DrReview.Modules.Review.Infrastructure.Review.Repositories.Interfaces
{
    using System.Threading.Tasks;

    public interface IReviewRepository
    {
        public Task<List<Entities.Review>> GetAllReviewsForRevieweeAsync(long revieweeId);

        public void InsertReview(Entities.Review review);

        public void UpdateReview(Entities.Review review);
    }
}
