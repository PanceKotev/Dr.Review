#nullable disable
namespace DrReview.Modules.User.Infrastructure.Common.Contexts
{
    using System.Threading.Tasks;
    using DrReview.Modules.User.Infrastructure.User.Configuration;
    using Microsoft.EntityFrameworkCore;

    public class UserReadOnlyDatabaseContext : DbContext
    {
        private readonly string SchemaName = "dbo";

        public UserReadOnlyDatabaseContext(DbContextOptions options) 
            : base(options)
        {
        }

        public virtual DbSet<User.Entities.User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new DbUpdateException("Cannot write to readonly database");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration(SchemaName));

        }

    }
}
