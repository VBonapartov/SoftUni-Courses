namespace BookShop.Reviews.Gateway.Services.ReviewViews
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.ReviewViews;
    using Refit;

    public interface IReviewViewService
    {
        [Get("/ReviewViews")]
        Task<IEnumerable<ReviewViewOutputModel>> TotalViews(
            [Query(CollectionFormat.Multi)] IEnumerable<int> ids);
    }
}