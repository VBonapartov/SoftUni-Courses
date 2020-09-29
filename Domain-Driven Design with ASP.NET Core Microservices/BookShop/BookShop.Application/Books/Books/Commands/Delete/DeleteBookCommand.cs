namespace BookShop.Application.Books.Books.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Common.Contracts;   
    using Common;
    using Domain.Books.Repositories;
    using MediatR;    

    public class DeleteBookCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IBookDomainRepository bookRepository;
            private readonly IAuthorDomainRepository authorRepository;

            public DeleteBookCommandHandler(
                ICurrentUser currentUser,
                IBookDomainRepository bookRepository,
                IAuthorDomainRepository authorRepository)
            {
                this.currentUser = currentUser;
                this.bookRepository = bookRepository;
                this.authorRepository = authorRepository;
            }

            public async Task<Result> Handle(
                DeleteBookCommand request,
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

                return await this.bookRepository.Delete(
                    request.Id,
                    cancellationToken);
            }
        }
    }
}