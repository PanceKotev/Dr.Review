#nullable disable
namespace DrReview.Modules.Review.Infrastructure.Common.UnitOfWork
{
    using DrReview.Common.Infrastructure.UnitOfWork;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Modules.Review.Infrastructure.Common.Contexts;
    using DrReview.Modules.Review.Infrastructure.Common.UnitOfWork.Interfaces;
    using DrReview.Modules.Review.Infrastructure.Review.Repositories;
    using DrReview.Modules.Review.Infrastructure.Review.Repositories.Interfaces;

    public class ReviewUnitOfWork : BaseUnitOfWork, IReviewUnitOfWork
    {
        private IReviewRepository _reviews;

        public ReviewUnitOfWork(ReviewDatabaseContext databaseContext, IDrReviewMediatorService mediatorService)
            : base(databaseContext, mediatorService)
        {
        }

        public IReviewRepository Reviews
        {
            get
            {
                if (_reviews is null)
                {
                    _reviews = new ReviewRepository(DatabaseContext);
                }

                return _reviews;
            }
        }
    }
}
