namespace BookShop.Application.Features.Books.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using BookShop.Application.Contracts;
    using BookShop.Application.Features.Authors;
    using BookShop.Domain.Common;
    using BookShop.Domain.Factories.Books;
    using BookShop.Domain.Models.Books;
    using Common;
    using MediatR;    

    public class CreateBookCommand : BookCommand<CreateBookCommand>, IRequest<CreateBookOutputModel>
    {
        public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CreateBookOutputModel>
        {
            private readonly ICurrentUser currentUser;
            private readonly IAuthorRepository authorRepository;
            private readonly IBookRepository bookRepository;
            private readonly IBookFactory bookFactory;

            public CreateBookCommandHandler(
                ICurrentUser currentUser,
                IAuthorRepository authorRepository,
                IBookRepository bookRepository,
                IBookFactory bookFactory)
            {
                this.currentUser = currentUser;
                this.authorRepository = authorRepository;
                this.bookRepository = bookRepository;
                this.bookFactory = bookFactory;
            }

            public async Task<CreateBookOutputModel> Handle(
                CreateBookCommand request,
                CancellationToken cancellationToken)
            {
                var author = await this.authorRepository.FindByUser(
                    this.currentUser.UserId,
                    cancellationToken);

                var publisher = await this.bookRepository.GetPublisher(
                    request.Publisher,
                    cancellationToken);

                //var factory = author == null
                //    ? this.bookFactory.WithAuthor(request.Author)
                //    : this.bookFactory.WithAuthor(author);

                var book = this.bookFactory
                    .WithTitle(request.Title)
                    .WithPublisher(publisher)
                    .WithPrice(request.Price)
                    .WithOptions(
                        request.NumberOfPages,
                        Enumeration.FromValue<CoverType>(request.CoverType),
                        Enumeration.FromValue<CategoryType>(request.CategoryType))
                    .Build();

                author.AddBook(book);

                await this.bookRepository.Save(book, cancellationToken);

                return new CreateBookOutputModel(book.Id);
            }
        }
    }
}