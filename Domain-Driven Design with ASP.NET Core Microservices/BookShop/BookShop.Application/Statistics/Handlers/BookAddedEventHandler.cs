namespace BookShop.Application.Statistics.Handlers
{
    using System.Threading.Tasks;    
    using Common;
    using Domain.Books.Events.Books;
    using Domain.Statistics.Repositories;

    public class BookAddedEventHandler : IEventHandler<BookAddedEvent>
    {
        private readonly IStatisticsDomainRepository statistics;

        public BookAddedEventHandler(IStatisticsDomainRepository statistics)
            => this.statistics = statistics;

        public Task Handle(BookAddedEvent domainEvent)
            => this.statistics.IncrementBooks();
    }
}
