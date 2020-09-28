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
        [Route(Id)]
        public async Task<ActionResult<GetCurrentStatisticsOutputModel>> Current(
            [FromRoute] GetCurrentStatisticsQuery query)
            => await this.Send(query);

        [HttpPut]
        [Route(Id)]
        public async Task<ActionResult<int>> BookViews(
             [FromRoute] GetBookViewsQuery query)
             => await this.Send(query);       
    }
}
