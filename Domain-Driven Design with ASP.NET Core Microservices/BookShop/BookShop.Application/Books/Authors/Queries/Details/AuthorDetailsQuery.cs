namespace BookShop.Application.Books.Authors.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class AuthorDetailsQuery : IRequest<AuthorDetailsOutputModel>
    {
        public int Id { get; set; }

        public class AuthorDetailsQueryHandler : IRequestHandler<AuthorDetailsQuery, AuthorDetailsOutputModel>
        {
            private readonly IAuthorRepository authorRepository;

            public AuthorDetailsQueryHandler(IAuthorRepository authorRepository)
                => this.authorRepository = authorRepository;

            public async Task<AuthorDetailsOutputModel> Handle(
                AuthorDetailsQuery request,
                CancellationToken cancellationToken)
                => await this.authorRepository.GetDetails(request.Id, cancellationToken);
        }
    }
}