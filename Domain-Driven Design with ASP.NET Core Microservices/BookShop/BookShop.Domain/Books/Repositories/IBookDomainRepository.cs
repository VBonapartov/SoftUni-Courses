namespace BookShop.Domain.Books.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Common;
    using Models.Books;

    public interface IBookDomainRepository : IDomainRepository<Book>
    {
        Task<Book> Find(int id, CancellationToken cancellationToken = default);

        Task<bool> Delete(int id, CancellationToken cancellationToken = default);

        Task<Publisher> GetPublisher(
            string publisher,
            CancellationToken cancellationToken = default);
    }
}
