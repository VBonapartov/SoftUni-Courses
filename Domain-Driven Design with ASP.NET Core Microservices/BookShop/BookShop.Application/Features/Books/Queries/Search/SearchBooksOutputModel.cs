namespace BookShop.Application.Features.Books.Queries.Search
{
    using System.Collections.Generic;
    using BookShop.Application.Features.Books.Queries.Common;

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