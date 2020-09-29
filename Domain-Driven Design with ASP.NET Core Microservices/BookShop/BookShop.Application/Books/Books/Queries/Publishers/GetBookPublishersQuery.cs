namespace BookShop.Application.Books.Books.Queries.Publishers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class GetBookPublishersQuery : IRequest<IEnumerable<GetBookPublisherOutputModel>>
    {
        public class GetBookAuthorsQueryHandler : IRequestHandler<
            GetBookPublishersQuery,
            IEnumerable<GetBookPublisherOutputModel>>
        {
            private readonly IBookQueryRepository bookRepository;

            public GetBookAuthorsQueryHandler(IBookQueryRepository bookRepository)
                => this.bookRepository = bookRepository;

            public async Task<IEnumerable<GetBookPublisherOutputModel>> Handle(
                GetBookPublishersQuery request,
                CancellationToken cancellationToken)
                => await this.bookRepository.GetBookPublishers(cancellationToken);
        }
    }
}