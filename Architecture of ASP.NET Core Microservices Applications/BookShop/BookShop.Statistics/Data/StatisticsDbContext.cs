namespace BookShop.Statistics.Data
{
    using System.Reflection;
    using BookShop.Statistics.Data.Models;
    using Microsoft.EntityFrameworkCore;
    
    public class StatisticsDbContext : DbContext
    {
        public StatisticsDbContext(DbContextOptions<StatisticsDbContext> options)
            : base(options)
        {
        }

        public DbSet<BookView> BookView { get; set; }

        public DbSet<ReviewView> ReviewView { get; set; }

        public DbSet<Statistics> Statistics { get; set; }        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}