namespace BookShop.Statistics.Messages
{
    using System.Threading.Tasks;
    using BookShop.Messages.Books;
    using BookShop.Statistics.Services.Statistics;
    using MassTransit;

    public class BookCreatedConsumer : IConsumer<BookCreatedMessage>
    {
        private readonly IStatisticsService statistics;

        public BookCreatedConsumer(IStatisticsService statistics)
            => this.statistics = statistics;

        public async Task Consume(ConsumeContext<BookCreatedMessage> context)
            => await this.statistics.AddBook();
    }
}