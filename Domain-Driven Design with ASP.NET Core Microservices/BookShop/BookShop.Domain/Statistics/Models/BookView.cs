namespace BookShop.Domain.Statistics.Models
{
    using Common.Models;

    public class BookView : Entity<int>
    {
        internal BookView(int bookId, string? userId)
        {
            this.BookId = bookId;
            this.UserId = userId;
        }

        public int BookId { get; }

        public string? UserId { get; }
    }
}
