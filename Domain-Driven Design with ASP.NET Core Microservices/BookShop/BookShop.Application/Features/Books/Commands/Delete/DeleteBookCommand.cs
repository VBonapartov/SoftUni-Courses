namespace BookShop.Application.Features.Books.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using BookShop.Application.Contracts;
    using BookShop.Application.Features.Authors;
    using BookShop.Application.Features.Books.Commands.Common;
    using MediatR;    

    public class DeleteBookCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IBookRepository bookRepository;
            private readonly IAuthorRepository authorRepository;

            public DeleteBookCommandHandler(
                ICurrentUser currentUser,
                IBookRepository bookRepository,
                IAuthorRepository authorRepository)
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