namespace BookShop.Application.Books.Books.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Contracts;    
    using Common;    
    using Domain.Books.Factories.Books;
    using Domain.Books.Models.Books;
    using Domain.Books.Repositories;
    using Domain.Common.Models;
    using MediatR;    

    public class CreateBookCommand : BookCommand<CreateBookCommand>, IRequest<CreateBookOutputModel>
    {
        public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CreateBookOutputModel>
        {
            private readonly ICurrentUser currentUser;
            private readonly IAuthorDomainRepository authorRepository;
            private readonly IBookDomainRepository bookRepository;
            private readonly IBookFactory bookFactory;

            public CreateBookCommandHandler(
                ICurrentUser currentUser,
                IAuthorDomainRepository authorRepository,
                IBookDomainRepository bookRepository,
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