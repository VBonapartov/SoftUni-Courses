namespace BookShop.Books.Services.ReviewsGateway
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.ReviewsGateway;
    using Refit;

    public interface IReviewsGatewayService
    {
        [Get("/Reviews/Mine")]
        Task<IEnumerable<MineReviewOutputModel>> Mine();
    }
}