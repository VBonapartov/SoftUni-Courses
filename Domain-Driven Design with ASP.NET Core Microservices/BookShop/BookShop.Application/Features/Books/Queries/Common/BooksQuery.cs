namespace BookShop.Application.Features.Books.Queries.Common
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using BookShop.Domain.Models.Authors;
    using BookShop.Domain.Models.Books;
    using BookShop.Domain.Specifications;
    using BookShop.Domain.Specifications.Authors;
    using BookShop.Domain.Specifications.Books;

    public abstract class BooksQuery
    {
        private const int BooksPerPage = 10;

        public string? Title { get; set; }

        public string? Author { get; set; }

        public string? Publisher { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string? SortBy { get; set; }

        public string? Order { get; set; }

        public int Page { get; set; } = 1;

        public abstract class BooksQueryHandler
        {
            private readonly IBookRepository bookRepository;

            protected BooksQueryHandler(IBookRepository bookRepository)
                => this.bookRepository = bookRepository;

            protected async Task<IEnumerable<TOutputModel>> GetBookListings<TOutputModel>(
                BooksQuery request,
                bool onlyAvailable = true,
                int? authorId = default,
                CancellationToken cancellationToken = default)
            {
                var bookSpecification = this.GetBookSpecification(request, onlyAvailable);

                var authorSpecification = this.GetAuthorSpecification(request, authorId);

                var searchOrder = new BooksSortOrder(request.SortBy, request.Order);

                var skip = (request.Page - 1) * BooksPerPage;

                return await this.bookRepository.GetBookListings<TOutputModel>(
                    bookSpecification,
                    authorSpecification,
                    searchOrder,
                    skip,
                    take: BooksPerPage,
                    cancellationToken);
            }

            protected async Task<int> GetTotalPages(
                BooksQuery request,
                bool onlyAvailable = true,
                int? authorId = default,
                CancellationToken cancellationToken = default)
            {
                var bookSpecification = this.GetBookSpecification(request, onlyAvailable);

                var authorSpecification = this.GetAuthorSpecification(request, authorId);

                var totalBooks = await this.bookRepository.Total(
                    bookSpecification,
                    authorSpecification,
                    cancellationToken);

                return (int)Math.Ceiling((double)totalBooks / BooksPerPage);
            }

            private Specification<Book> GetBookSpecification(BooksQuery request, bool onlyAvailable)
                => new BookByPublisherSpecification(request.Publisher)
                    .And(new BookByPriceSpecification(request.MinPrice, request.MaxPrice))
                    .And(new BookOnlyAvailableSpecification(onlyAvailable));

            private Specification<Author> GetAuthorSpecification(BooksQuery request, int? authorId)
                => new AuthorByIdSpecification(authorId)
                    .And(new AuthorByNameSpecification(request.Author));
        }
    }
}