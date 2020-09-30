namespace BookShop.Domain.Statistics.Repositories
{    
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Common;
    using Domain.Statistics.Models;

    public interface IStatisticsDomainRepository : IDomainRepository<Statistics>
    {
        Task IncrementBooks(CancellationToken cancellationToken = default);
    }
}
