namespace BookShop.Infrastructure.Statistics.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Statistics;
    using Application.Statistics.Queries.Current;
    using AutoMapper;   
    using Common.Persistence;
    using Domain.Statistics.Models;
    using Domain.Statistics.Repositories;
    using Microsoft.EntityFrameworkCore;

    internal class StatisticsRepository : DataRepository<IStatisticsDbContext, Statistics>, IStatisticsQueryRepository, IStatisticsDomainRepository
    {
        private readonly IMapper mapper;

        public StatisticsRepository(IStatisticsDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public async Task<GetCurrentStatisticsOutputModel> GetCurrent(CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<GetCurrentStatisticsOutputModel>(this.All())
                .SingleOrDefaultAsync(cancellationToken);

        public async Task<int> GetBookViews(int bookId, CancellationToken cancellationToken = default)
            => await this.Data
                .BookViews
                .CountAsync(bv => bv.BookId == bookId, cancellationToken);

        public async Task IncrementBooks(CancellationToken cancellationToken = default)
        {
            var statistics = await this.Data
                .Statistics
                .SingleOrDefaultAsync(cancellationToken);

            statistics.AddBook();

            await this.Save(statistics, cancellationToken);
        }
    }
}
