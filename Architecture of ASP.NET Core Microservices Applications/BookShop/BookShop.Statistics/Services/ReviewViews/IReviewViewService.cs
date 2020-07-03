namespace BookShop.Statistics.Services.ReviewViews
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookShop.Statistics.Models.ReviewViews;

    public interface IReviewViewService
    {
        Task<int> GetTotalViews(int carAdId);

        Task<IEnumerable<ReviewViewOutputModel>> GetTotalViews(IEnumerable<int> ids);

        Task ViewReview(int reviewId, string userId);
    }
}