namespace BookShop.Application.Statistics
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Contracts;
    using Domain.Statistics.Models;
    using Queries.Current;   

    public interface IStatisticsQueryRepository : IQueryRepository<Statistics>
    {
        Task<GetCurrentStatisticsOutputModel> GetCurrent(CancellationToken cancellationToken = default);

        Task<int> GetBookViews(int bookId, CancellationToken cancellationToken = default);        
    }
}
