namespace BookShop.Books.Services.Reviews
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookShop.Books.Services.Models.Reviews;
    using Refit;

    public interface IReviewService
    {
        [Get("/Reviews/All")]
        Task<IEnumerable<ReviewListingServiceModel>> All();

        [Get("/Reviews/Details/{id}")]
        Task<ReviewListingServiceModel> Details(int id);

        [Post("/Reviews/Add")]
        Task Add([Body] ReviewListingServiceModel model);

        [Put("/Reviews/Edit/{id}")]
        Task Edit(int id, [Body] ReviewListingServiceModel model);

        [Delete("/Reviews/Delete/{id}")]
        Task Delete(int id);
    }
}