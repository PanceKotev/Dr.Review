namespace DrReview.Modules.Review.Infrastructure.Common.UnitOfWork.Interfaces
{
    using System.Threading.Tasks;
    using DrReview.Modules.Review.Infrastructure.Review.Repositories.Interfaces;

    public interface IReviewUnitOfWork
    {
        public IReviewRepository Reviews { get; }

        public Task SaveAsync();
    }
}
