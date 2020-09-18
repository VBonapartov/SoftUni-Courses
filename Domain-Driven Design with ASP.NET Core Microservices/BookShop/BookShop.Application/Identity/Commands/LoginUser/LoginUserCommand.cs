namespace BookShop.Application.Identity.Commands.LoginUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Books.Authors;
    using Common;
    using MediatR;

    public class LoginUserCommand : UserInputModel, IRequest<Result<LoginOutputModel>>
    {
        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentity identity;
            private readonly IAuthorRepository authorRepository;

            public LoginUserCommandHandler(
                IIdentity identity,
                IAuthorRepository authorRepository)
            {
                this.identity = identity;
                this.authorRepository = authorRepository;
            }

            public async Task<Result<LoginOutputModel>> Handle(
                LoginUserCommand request,
                CancellationToken cancellationToken)
            {
                var result = await this.identity.Login(request);

                if (!result.Succeeded)
                {
                    return result.Errors;
                }

                var user = result.Data;

                var authorId = await this.authorRepository.GetAuthorId(user.UserId, cancellationToken);

                return new LoginOutputModel(user.Token, authorId);
            }
        }
    }
}