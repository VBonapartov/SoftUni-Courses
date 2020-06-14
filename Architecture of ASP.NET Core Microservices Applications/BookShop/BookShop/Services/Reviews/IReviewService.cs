namespace BookShop.Services.Reviews
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookShop.Services.Models.Reviews;

    public interface IReviewService
    {
        Task<IEnumerable<ReviewListingServiceModel>> All();

        Task<ReviewListingServiceModel> Details(int id);

        Task<bool> Exists(int id);

        Task Create(
                    string title,
                    string description,
                    string author,
                    int bookId);

        Task Update(
                    int id,
                    string title,
                    string description,
                    string author,
                    int bookId);

        Task Delete(int id);
    }
}