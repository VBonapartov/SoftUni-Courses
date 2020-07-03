namespace BookShop.Books.Controllers
{
    using System.Threading.Tasks;
    using BookShop.Books.Services.ReviewsGateway;
    using BookShop.Books.Services.Statistics;
    using BookShop.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class StatisticsController : Controller
    {
        private readonly IStatisticsService statistics;

        private readonly IReviewsGatewayService reviews;

        public StatisticsController(IStatisticsService statistics, IReviewsGatewayService reviews)
        {
            this.statistics = statistics;
            this.reviews = reviews;
        }

        [AuthorizeAdministrator]
        public async Task<IActionResult> Index()
            => View(await this.statistics.Full());

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Reviews()
            => View(await this.reviews.Mine());
    }
}