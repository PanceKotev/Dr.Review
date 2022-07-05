#nullable disable
namespace DrReview.Modules.User.Infrastructure.Common.Contexts
{
    using DrReview.Modules.User.Infrastructure.User.Configuration;
    using Microsoft.EntityFrameworkCore;

    public class UserDatabaseContext : DbContext
    {
        private const string SchemaName = "dbo";

        public UserDatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<User.Entities.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration(SchemaName));
        }
    }
}
