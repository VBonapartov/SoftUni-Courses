namespace BookShop.Application.Features.Books
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using BookShop.Application.Contracts;
    using BookShop.Application.Features.Books.Queries.Common;
    using BookShop.Application.Features.Books.Queries.Details;
    using BookShop.Application.Features.Books.Queries.Publishers;
    using BookShop.Domain.Models.Authors;
    using BookShop.Domain.Models.Books;
    using BookShop.Domain.Specifications;

    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> Find(int id, CancellationToken cancellationToken = default);

        Task<bool> Delete(int id, CancellationToken cancellationToken = default);

        Task<Publisher> GetPublisher(
            string publisher,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<TOutputModel>> GetBookListings<TOutputModel>(
            Specification<Book> bookSpecification,
            Specification<Author> authorSpecification,
            BooksSortOrder booksSortOrder,
            int skip = 0,
            int take = int.MaxValue,
            CancellationToken cancellationToken = default);

        Task<BookDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<GetBookPublisherOutputModel>> GetBookPublishers(
            CancellationToken cancellationToken = default);

        Task<int> Total(
            Specification<Book> bookSpecification,
            Specification<Author> authorSpecification,
            CancellationToken cancellationToken = default);
    }
}