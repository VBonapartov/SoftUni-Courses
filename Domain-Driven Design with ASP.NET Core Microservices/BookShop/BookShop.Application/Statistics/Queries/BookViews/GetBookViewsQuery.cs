namespace BookShop.Application.Statistics.Queries.BookViews
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class GetBookViewsQuery : IRequest<int>
    {
        public int BookId { get; set; }

        public class GetBookViewsQueryHandler : IRequestHandler<GetBookViewsQuery, int>
        {
            private readonly IStatisticsQueryRepository statistics;

            public GetBookViewsQueryHandler(IStatisticsQueryRepository statistics)
                => this.statistics = statistics;

            public Task<int> Handle(
                GetBookViewsQuery request,
                CancellationToken cancellationToken)
                => this.statistics.GetBookViews(request.BookId, cancellationToken);
        }
    }
}
