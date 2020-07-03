namespace BookShop.Reviews.Services.Reviews
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookShop.Reviews.Models.Reviews;

    public interface IReviewService
    {
        Task<IEnumerable<ReviewOutputModel>> All();

        Task<ReviewOutputModel> Details(int id);

        Task<IEnumerable<ReviewOutputModel>> Mine(string authorId);

        Task<bool> Exists(int id);

        Task Create(
                    string title,
                    string description,
                    string authorId,
                    string author,
                    int bookId,
                    string bookName);

        Task Update(
                    int id,
                    string title,
                    string description,
                    string authorId,
                    string author,
                    int bookId,
                    string bookName);

        Task Delete(int id);
    }
}