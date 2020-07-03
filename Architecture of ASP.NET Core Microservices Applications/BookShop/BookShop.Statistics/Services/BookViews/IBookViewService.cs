namespace BookShop.Statistics.Services.BookViews
{
    using System.Threading.Tasks;

    public interface IBookViewService
    {
        Task<int> GetTotalViews(int BookId);

        Task ViewBook(int bookId, string userId);
    }
}