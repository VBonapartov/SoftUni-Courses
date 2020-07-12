namespace BookShop.Reviews.Data
{
    using System.Reflection;
    using BookShop.Data;
    using BookShop.Reviews.Data.Models;
    using Microsoft.EntityFrameworkCore;
    
    public class ReviewsDbContext : MessageDbContext
    {
        public ReviewsDbContext(DbContextOptions<ReviewsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Review> Reviews { get; set; }

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
    }
}