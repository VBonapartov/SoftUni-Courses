namespace BookShop.Domain.Books.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Books.Models.Authors;
    using Domain.Common;

    public interface IAuthorDomainRepository : IDomainRepository<Author>
    {
        Task<Author> FindByUser(string userId, CancellationToken cancellationToken = default);

        Task<int> GetAuthorId(string userId, CancellationToken cancellationToken = default);

        Task<bool> HasBook(int authorId, int bookId, CancellationToken cancellationToken = default);
    }
}
