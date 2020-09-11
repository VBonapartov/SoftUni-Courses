namespace BookShop.Application.Features.Books.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using BookShop.Application.Features.Authors;
    using MediatR;

    public class BookDetailsQuery : EntityCommand<int>, IRequest<BookDetailsOutputModel>
    {
        public class BookDetailsQueryHandler : IRequestHandler<BookDetailsQuery, BookDetailsOutputModel>
        {
            private readonly IBookRepository bookRepository;
            private readonly IAuthorRepository authorRepository;

            public BookDetailsQueryHandler(
                IBookRepository bookRepository,
                IAuthorRepository authorRepository)
            {
                this.bookRepository = bookRepository;
                this.authorRepository = authorRepository;
            }

            public async Task<BookDetailsOutputModel> Handle(
                BookDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var bookDetails = await this.bookRepository.GetDetails(
                    request.Id,
                    cancellationToken);

                bookDetails.Author = await this.authorRepository.GetDetailsByBookId(
                    request.Id,
                    cancellationToken);

                return bookDetails;
            }
        }
    }
}