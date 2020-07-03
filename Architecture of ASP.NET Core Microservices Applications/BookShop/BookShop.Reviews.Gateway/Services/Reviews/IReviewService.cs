namespace BookShop.Reviews.Gateway.Services.Reviews
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookShop.Reviews.Gateway.Models.Reviews;
    using Refit;

    public interface IReviewService
    {
        [Get("/Reviews/Mine")]
        Task<IEnumerable<ReviewOutputModel>> Mine();
    }
}