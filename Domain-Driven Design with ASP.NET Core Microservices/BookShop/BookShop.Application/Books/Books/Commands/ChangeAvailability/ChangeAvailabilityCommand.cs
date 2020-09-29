namespace BookShop.Application.Books.Books.Commands.ChangeAvailability
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Common.Contracts;    
    using Commands.Common;
    using Domain.Books.Repositories;
    using MediatR;   

    public class ChangeAvailabilityCommand : EntityCommand<int>, IRequest<Result>
    {
        public class ChangeAvailabilityCommandHandler : IRequestHandler<ChangeAvailabilityCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IBookDomainRepository bookRepository;
            private readonly IAuthorDomainRepository authorRepository;

            public ChangeAvailabilityCommandHandler(
                ICurrentUser currentUser,
                IBookDomainRepository bookRepository,
                IAuthorDomainRepository authorRepository)
            {
                this.currentUser = currentUser;
                this.bookRepository = bookRepository;
                this.authorRepository = authorRepository;
            }

            public async Task<Result> Handle(
                ChangeAvailabilityCommand request,
                CancellationToken cancellationToken)
            {
                var authorHasBook = await this.currentUser.AuthorHasBook(
                    this.authorRepository,
                    request.Id,
                    cancellationToken);

                if (!authorHasBook)
                {
                    return authorHasBook;
                }

                var book = await this.bookRepository
                    .Find(request.Id, cancellationToken);

                book.ChangeAvailability();

                await this.bookRepository.Save(book, cancellationToken);

                return Result.Success;
            }
        }
    }
}