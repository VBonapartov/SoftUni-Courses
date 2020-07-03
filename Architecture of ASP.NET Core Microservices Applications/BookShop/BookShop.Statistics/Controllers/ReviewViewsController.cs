namespace BookShop.Statistics.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookShop.Controllers;
    using BookShop.Statistics.Models.ReviewViews;
    using BookShop.Statistics.Services.ReviewViews;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;    

    public class ReviewViewsController : ApiController
    {
        private readonly IReviewViewService reviewViews;

        public ReviewViewsController(IReviewViewService reviewViews)
        {
            this.reviewViews = reviewViews;
        }

        [HttpGet]
        [Route(nameof(TotalViews) + "/" + Id)]
        public async Task<ActionResult<int>> TotalViews(int id)
            => await this.reviewViews.GetTotalViews(id);

        [HttpGet]
        [Authorize]
        //[Route(nameof(TotalViews))]
        public async Task<IEnumerable<ReviewViewOutputModel>> TotalViews(
            [FromQuery] IEnumerable<int> ids)
            => await this.reviewViews.GetTotalViews(ids);
    }
}