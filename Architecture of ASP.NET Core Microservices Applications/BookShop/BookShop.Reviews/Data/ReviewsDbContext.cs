namespace BookShop.Reviews.Data
{
    using System.Reflection;
    using BookShop.Reviews.Data.Models;
    using Microsoft.EntityFrameworkCore;
    
    public class ReviewsDbContext : DbContext
    {
        public ReviewsDbContext(DbContextOptions<ReviewsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}