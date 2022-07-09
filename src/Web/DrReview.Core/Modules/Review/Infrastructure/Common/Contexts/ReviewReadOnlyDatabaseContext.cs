#nullable disable
namespace DrReview.Modules.Review.Infrastructure.Common.Contexts
{
    using System.Threading.Tasks;
    using DrReview.Modules.Review.Infrastructure.Review.Configuration;
    using Microsoft.EntityFrameworkCore;

    public class ReviewReadOnlyDatabaseContext : DbContext
    {
        private readonly string _schemaName = "dbo";

        public ReviewReadOnlyDatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Review.Entities.Reviewee> Reviewers { get; set; }

        public virtual DbSet<Review.Entities.Reviewee> Reviewees { get; set; }

        public virtual DbSet<Review.Entities.Review> Reviews { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new DbUpdateException("Cannot write to readonly database");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ReviewerConfiguration(_schemaName));
            modelBuilder.ApplyConfiguration(new RevieweeConfiguration(_schemaName));
            modelBuilder.ApplyConfiguration(new ReviewConfiguration(_schemaName));
        }
    }
}
