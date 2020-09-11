namespace BookShop.Application.Features.Books.Queries.Search
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using MediatR;

    public class SearchBooksQuery : BooksQuery, IRequest<SearchBooksOutputModel>
    {
        public class SearchBooksQueryHandler : BooksQueryHandler, IRequestHandler<
            SearchBooksQuery,
            SearchBooksOutputModel>
        {
            public SearchBooksQueryHandler(IBookRepository bookRepository)
                : base(bookRepository)
            {
            }

            public async Task<SearchBooksOutputModel> Handle(
                SearchBooksQuery request,
                CancellationToken cancellationToken)
            {
                var bookListings = await base.GetBookListings<BookOutputModel>(
                    request,
                    cancellationToken: cancellationToken);

                var totalPages = await base.GetTotalPages(
                    request,
                    cancellationToken: cancellationToken);

                return new SearchBooksOutputModel(bookListings, request.Page, totalPages);
            }
        }
    }
}