namespace BookShop.Statistics.Services.BookViews
{
    using System.Threading.Tasks;
    using BookShop.Statistics.Data;
    using BookShop.Statistics.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class BookViewService : IBookViewService
    {
        private readonly StatisticsDbContext db;

        public BookViewService(StatisticsDbContext db)
        {
            this.db = db;
        }

        public async Task<int> GetTotalViews(int BookId)
            => await this.db
                .BookView
                .CountAsync(v => v.BookId == BookId);


        public async Task ViewBook(int bookId, string userId)
        {
            var bookView = new BookView
            {
                BookId = bookId,
                UserId = userId
            };

            await this.db.BookView.AddAsync(bookView);
            await this.db.SaveChangesAsync();
        }
    }
}