namespace BookShop.Statistics.Services.Statistics
{
    using System.Threading.Tasks;
    using AutoMapper;
    using BookShop.Statistics.Data;
    using BookShop.Statistics.Models.Statistics;
    using Microsoft.EntityFrameworkCore;

    public class StatisticsService : IStatisticsService
    {
        private readonly StatisticsDbContext db;

        private readonly IMapper mapper;

        public StatisticsService(StatisticsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<StatisticsOutputModel> Full()
            => await this.mapper
                .ProjectTo<StatisticsOutputModel>(this.db.Statistics)
                .SingleOrDefaultAsync();

        public async Task AddBook()
        {
            var statistics = await this.db.Statistics.SingleOrDefaultAsync();
            statistics.TotalBooks++;

            await this.db.SaveChangesAsync();
        }

        public async Task AddReview()
        {
            var statistics = await this.db.Statistics.SingleOrDefaultAsync();
            statistics.TotalReviews++;

            await this.db.SaveChangesAsync();
        }
    }
}