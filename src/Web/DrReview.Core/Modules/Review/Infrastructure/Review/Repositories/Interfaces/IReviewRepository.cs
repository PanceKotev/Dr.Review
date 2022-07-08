namespace DrReview.Modules.Review.Infrastructure.Review.Repositories.Interfaces
{
    using System.Threading.Tasks;

    public interface IReviewRepository
    {
        public Task<List<Entities.Review>> GetAllReviewsForRevieweeAsync(long revieweeId);
    }
}
