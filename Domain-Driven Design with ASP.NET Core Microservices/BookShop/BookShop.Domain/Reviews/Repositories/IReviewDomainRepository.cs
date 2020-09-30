namespace BookShop.Domain.Reviews.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Common;
    using Domain.Reviews.Models;

    public interface IReviewDomainRepository : IDomainRepository<Review>
    {
        Task<Review> Find(int id, CancellationToken cancellationToken = default);

        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
    }
}
