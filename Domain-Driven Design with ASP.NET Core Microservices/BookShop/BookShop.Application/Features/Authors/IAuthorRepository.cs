namespace BookShop.Application.Features.Authors
{
    using System.Threading;
    using System.Threading.Tasks;
    using BookShop.Application.Contracts;
    using BookShop.Application.Features.Authors.Queries.Common;
    using BookShop.Application.Features.Authors.Queries.Details;
    using BookShop.Domain.Models.Authors;

    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> FindByUser(string userId, CancellationToken cancellationToken = default);

        Task<int> GetAuthorId(string userId, CancellationToken cancellationToken = default);

        Task<bool> HasBook(int authorId, int bookId, CancellationToken cancellationToken = default);

        Task<AuthorDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default);

        Task<AuthorOutputModel> GetDetailsByBookId(int bookId, CancellationToken cancellationToken = default);
    }
}