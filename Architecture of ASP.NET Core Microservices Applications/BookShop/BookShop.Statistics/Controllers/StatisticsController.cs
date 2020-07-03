namespace BookShop.Statistics.Controllers
{
    using System.Threading.Tasks;
    using BookShop.Controllers;
    using BookShop.Statistics.Models.Statistics;
    using BookShop.Statistics.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;    

    public class StatisticsController : ApiController
    {
        private readonly IStatisticsService statistics;

        public StatisticsController(IStatisticsService statistics)
        {
            this.statistics = statistics;
        }

        [HttpGet]
        [Route(nameof(Full))]
        public async Task<StatisticsOutputModel> Full()
            => await this.statistics.Full();
    }
}