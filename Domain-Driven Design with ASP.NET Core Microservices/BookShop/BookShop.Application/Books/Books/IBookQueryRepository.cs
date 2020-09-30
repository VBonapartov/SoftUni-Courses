namespace BookShop.Application.Books.Books
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Contracts;
    using Domain.Books.Models.Authors;
    using Domain.Books.Models.Books;
    using Domain.Common;
    using Queries.Common;
    using Queries.Details;
    using Queries.Publishers;

    public interface IBookQueryRepository : IQueryRepository<Book>
    {
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