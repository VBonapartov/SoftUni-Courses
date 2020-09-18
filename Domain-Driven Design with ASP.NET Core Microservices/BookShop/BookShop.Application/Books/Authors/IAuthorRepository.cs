namespace BookShop.Application.Books.Authors
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Contracts;
    using Domain.Books.Models.Authors;
    using Queries.Common;
    using Queries.Details;    

    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> FindByUser(string userId, CancellationToken cancellationToken = default);

        Task<int> GetAuthorId(string userId, CancellationToken cancellationToken = default);

        Task<bool> HasBook(int authorId, int bookId, CancellationToken cancellationToken = default);

        Task<AuthorDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default);

        Task<AuthorOutputModel> GetDetailsByBookId(int bookId, CancellationToken cancellationToken = default);
    }
}