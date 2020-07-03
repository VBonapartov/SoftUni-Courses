namespace BookShop.Reviews.Gateway.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using BookShop.Controllers;
    using BookShop.Reviews.Gateway.Models.Reviews;
    using BookShop.Reviews.Gateway.Services.Reviews;
    using BookShop.Reviews.Gateway.Services.ReviewViews;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewsController : ApiController
    {
        private readonly IReviewService reviews;

        private readonly IReviewViewService reviewViews;

        private readonly IMapper mapper;

        public ReviewsController(
            IReviewService reviews,
            IReviewViewService reviewViews,
            IMapper mapper
            )
        {
            this.reviews = reviews;
            this.reviewViews = reviewViews;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [Route(nameof(Mine))]
        public async Task<IEnumerable<MineReviewOutputModel>> Mine()
        {
            var mineReviews = await this.reviews.Mine();

            var mineReviewIds = mineReviews.Select(c => c.Id);

            var mineReviewViews = await this.reviewViews.TotalViews(mineReviewIds);

            var outputMineReviews =
                this.mapper
                    .Map<IEnumerable<MineReviewOutputModel>>(mineReviews)
                    .ToDictionary(c => c.Id);

            var mineReviewViewsDictionary = mineReviewViews
                .ToDictionary(v => v.ReviewId, v => v.TotalViews);

            foreach (var (reviewId, totalViews) in mineReviewViewsDictionary)
            {
                outputMineReviews[reviewId].TotalViews = totalViews;
            }

            return outputMineReviews.Values;
        }
    }
}