namespace BookShop.Application.Books.Books.Commands.Edit
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;    
    using Application.Common.Contracts;
    using Authors;
    using Common;
    using Domain.Common.Models;
    using Domain.Books.Models.Books;    
    using MediatR;

    public class EditBookCommand : BookCommand<EditBookCommand>, IRequest<Result>
    {
        public class EditBookCommandHandler : IRequestHandler<EditBookCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IBookRepository bookRepository;
            private readonly IAuthorRepository authorRepository;

            public EditBookCommandHandler(
                ICurrentUser currentUser,
                IBookRepository bookRepository,
                IAuthorRepository authorRepository)
            {
                this.currentUser = currentUser;
                this.bookRepository = bookRepository;
                this.authorRepository = authorRepository;
            }

            public async Task<Result> Handle(
                EditBookCommand request,
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

                book
                    .UpdateTitle(request.Title)
                    .UpdatePublisher(request.Publisher)
                    .UpdatePrice(request.Price)
                    .UpdateOptions(
                        request.NumberOfPages,
                        Enumeration.FromValue<CoverType>(request.CoverType),
                        Enumeration.FromValue<CategoryType>(request.CategoryType));

                await this.bookRepository.Save(book, cancellationToken);

                return Result.Success;
            }
        }
    }
}