namespace BookShop.Application.Books.Books.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Books.Authors;
    using Application.Common;
    using MediatR;

    public class BookDetailsQuery : EntityCommand<int>, IRequest<BookDetailsOutputModel>
    {
        public class BookDetailsQueryHandler : IRequestHandler<BookDetailsQuery, BookDetailsOutputModel>
        {
            private readonly IBookQueryRepository bookRepository;
            private readonly IAuthorQueryRepository authorRepository;

            public BookDetailsQueryHandler(
                IBookQueryRepository bookRepository,
                IAuthorQueryRepository authorRepository)
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