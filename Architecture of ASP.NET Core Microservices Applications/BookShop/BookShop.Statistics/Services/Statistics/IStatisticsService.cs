namespace BookShop.Statistics.Services.Statistics
{
    using System.Threading.Tasks;
    using BookShop.Statistics.Models.Statistics;

    public interface IStatisticsService
    {
        Task<StatisticsOutputModel> Full();

        Task AddBook();

        Task AddReview();
    }
}