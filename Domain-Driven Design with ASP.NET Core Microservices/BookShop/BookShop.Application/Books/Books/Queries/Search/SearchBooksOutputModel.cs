namespace BookShop.Application.Books.Books.Queries.Search
{
    using System.Collections.Generic;
    using Queries.Common;

    public class SearchBooksOutputModel : BooksOutputModel<BookOutputModel>
    {
        public SearchBooksOutputModel(
            IEnumerable<BookOutputModel> books,
            int page,
            int totalPages)
            : base(books, page, totalPages)
        {
        }
    }
}