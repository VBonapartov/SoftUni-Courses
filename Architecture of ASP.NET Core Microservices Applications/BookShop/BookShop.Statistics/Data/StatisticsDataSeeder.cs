namespace BookShop.Statistics.Data
{
    using System.Linq;
    using BookShop.Services;
    using BookShop.Statistics.Data.Models;

    public class StatisticsDataSeeder : IDataSeeder
    {
        private readonly StatisticsDbContext db;

        public StatisticsDataSeeder(StatisticsDbContext db) => this.db = db;

        public void SeedData()
        {
            if (this.db.Statistics.Any())
            {
                return;
            }

            this.db.Statistics.Add(new Statistics
            {
                TotalBooks = 0,
                TotalReviews = 0
            });

            this.db.SaveChanges();
        }
    }
}