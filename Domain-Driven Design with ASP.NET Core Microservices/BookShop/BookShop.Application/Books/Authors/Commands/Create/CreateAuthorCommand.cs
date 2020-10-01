namespace BookShop.Application.Books.Authors.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Contracts;
    using Domain.Books.Factories.Authors;
    using Domain.Books.Repositories;
    using MediatR;    

    public class CreateAuthorCommand : IRequest<CreateAuthorOutputModel>
    {
        public string Name { get; set; } = default!;

        public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, CreateAuthorOutputModel>
        {
            private readonly ICurrentUser currentUser;
            private readonly IAuthorDomainRepository authorRepository;
            private readonly IAuthorFactory authorFactory;

            public CreateAuthorCommandHandler(
                ICurrentUser currentUser,
                IAuthorDomainRepository authorRepository,
                IAuthorFactory authorFactory)
            {
                this.currentUser = currentUser;
                this.authorRepository = authorRepository;
                this.authorFactory = authorFactory;
            }

            public async Task<CreateAuthorOutputModel> Handle(
                CreateAuthorCommand request,
                CancellationToken cancellationToken)
            {
                var author = this.authorFactory
                    .WithName(request.Name)
                    .FromUser(this.currentUser.UserId)
                    .Build();

                await this.authorRepository.Save(author, cancellationToken);

                return new CreateAuthorOutputModel(author.Id);
            }
        }
    }
}