namespace BookShop.Web.Statistics
{
    using System.Threading.Tasks;
    using Application.Statistics.Queries.Current;
    using Application.Statistics.Queries.BookViews;
    using Microsoft.AspNetCore.Mvc;
    using Web.Common;

    public class StatisticsController : ApiController
    {
        [HttpGet]
        [Route(nameof(Current))]
        public async Task<ActionResult<GetCurrentStatisticsOutputModel>> Current(
            [FromRoute] GetCurrentStatisticsQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(nameof(BookViews))]
        public async Task<ActionResult<int>> BookViews(
             [FromRoute] GetBookViewsQuery query)
             => await this.Send(query);       
    }
}
