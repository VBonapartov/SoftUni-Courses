namespace BookShop.Application.Features.Books.Queries.Common
{
    using System.Collections.Generic;

    public abstract class BooksOutputModel<TBookOutputModel>
    {
        internal BooksOutputModel(
            IEnumerable<TBookOutputModel> books,
            int page,
            int totalPages)
        {
            this.Books = books;
            this.Page = page;
            this.TotalPages = totalPages;
        }

        public IEnumerable<TBookOutputModel> Books { get; }

        public int Page { get; }

        public int TotalPages { get; }
    }
}