﻿#nullable disable
namespace DrReview.Modules.Review.Infrastructure.Common.Contexts
{
    using DrReview.Modules.Review.Infrastructure.Review.Configuration;
    using Microsoft.EntityFrameworkCore;

    public class ReviewDatabaseContext : DbContext
    {
        private const string SchemaName = "dbo";

        public ReviewDatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Review.Entities.Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ReviewConfiguration(SchemaName));
        }
    }
}
