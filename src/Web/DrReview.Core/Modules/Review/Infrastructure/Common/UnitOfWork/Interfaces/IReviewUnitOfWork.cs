namespace DrReview.Modules.Review.Infrastructure.Common.UnitOfWork.Interfaces
{
    using System.Threading.Tasks;
    using DrReview.Modules.Review.Infrastructure.Review.Repositories.Interfaces;

    public interface IReviewUnitOfWork
    {
        public Task SaveAsync();

        public void Dispose();

        public IReviewRepository Reviews { get; }
    }
}
