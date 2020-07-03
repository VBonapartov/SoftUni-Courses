namespace BookShop.Books.Services.Statistics
{
    using System.Threading.Tasks;
    using Models.Statistics;
    using Refit;

    public interface IStatisticsService
    {
        [Get("/Statistics/Full")]
        Task<StatisticsOutputModel> Full();        
    }
}