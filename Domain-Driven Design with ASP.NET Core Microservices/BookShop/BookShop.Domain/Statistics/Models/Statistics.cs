namespace BookShop.Domain.Statistics.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    public class Statistics : IAggregateRoot
    {
        private readonly HashSet<BookView> bookViews;

        internal Statistics()
        {
            this.TotalBooks = 0;

            this.bookViews = new HashSet<BookView>();
        }

        public int TotalBooks { get; private set; }

        public IReadOnlyCollection<BookView> BookViews
            => this.bookViews.ToList().AsReadOnly();

        public void AddBook()
            => this.TotalBooks++;

        public void AddBookView(int bookId, string? userId)
            => this.bookViews.Add(new BookView(bookId, userId));
    }
}
