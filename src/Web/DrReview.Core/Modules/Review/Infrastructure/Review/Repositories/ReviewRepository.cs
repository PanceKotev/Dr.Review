namespace DrReview.Modules.Review.Infrastructure.Review.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DrReview.Common.Infrastructure.Repository;
    using DrReview.Modules.Review.Infrastructure.Review.Entities;
    using DrReview.Modules.Review.Infrastructure.Review.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ReviewRepository : BaseRepository<Entities.Review>, IReviewRepository
    {
        public ReviewRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<List<Review>> GetAllReviewsForRevieweeAsync(long revieweeId)
        {
            return await Query().Where(r => r.RevieweeFK == revieweeId)
                                .Include(x => x.Reviewee)
                                .Include(x => x.Reviewer)
                                .ToListAsync();
        }

        public void InsertReview(Entities.Review review)
        {
            Insert(review);
        }

        public void UpdateReview(Entities.Review review)
        {
            AttachOrUpdate(review, EntityState.Modified);
        }

        public void UpdateVote(Entities.Vote vote)
        {
            AttachOrUpdate<Entities.Vote>(vote, EntityState.Modified);
        }

        public void InsertVote(Entities.Vote vote)
        {
            InsertOf<Entities.Vote>(vote);
        }
    }
}
