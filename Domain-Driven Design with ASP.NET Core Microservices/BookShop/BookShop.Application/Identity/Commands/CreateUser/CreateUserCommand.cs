namespace BookShop.Application.Identity.Commands.CreateUser
{
    using System.Threading;
    using System.Threading.Tasks; 
    using Common;    
    using Domain.Books.Factories.Authors;
    using Domain.Books.Repositories;
    using MediatR;

    public class CreateUserCommand : UserInputModel, IRequest<Result>
    {
        public string Name { get; set; } = default!;

        //public string PhoneNumber { get; set; } = default!;

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
        {
            private readonly IIdentity identity;
            private readonly IAuthorFactory authorFactory;
            private readonly IAuthorDomainRepository authorRepository;

            public CreateUserCommandHandler(
                IIdentity identity,
                IAuthorFactory authorFactory,
                IAuthorDomainRepository authorRepository)
            {
                this.identity = identity;
                this.authorFactory = authorFactory;
                this.authorRepository = authorRepository;
            }

            public async Task<Result> Handle(
                CreateUserCommand request,
                CancellationToken cancellationToken)
            {
                var result = await this.identity.Register(request);

                if (!result.Succeeded)
                {
                    return result;
                }

                var user = result.Data;

                var author = this.authorFactory
                    .WithName(request.Name)
                    .Build();

                user.BecomeAuthor(author);

                await this.authorRepository.Save(author, cancellationToken);

                return result;
            }
        }
    }
}