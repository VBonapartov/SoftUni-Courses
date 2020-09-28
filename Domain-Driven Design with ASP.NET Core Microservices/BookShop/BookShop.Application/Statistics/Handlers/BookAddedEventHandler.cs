namespace BookShop.Application.Statistics.Handlers
{
    using System.Threading.Tasks;    
    using Common;
    using Domain.Books.Events.Books;

    public class BookAddedEventHandler : IEventHandler<BookAddedEvent>
    {
        private readonly IStatisticsRepository statistics;

        public BookAddedEventHandler(IStatisticsRepository statistics)
            => this.statistics = statistics;

        public Task Handle(BookAddedEvent domainEvent)
            => this.statistics.IncrementBooks();
    }
}
