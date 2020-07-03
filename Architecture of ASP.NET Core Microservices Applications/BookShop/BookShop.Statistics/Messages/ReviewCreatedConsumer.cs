namespace BookShop.Statistics.Messages
{
    using System.Threading.Tasks;
    using BookShop.Messages.Reviews;
    using BookShop.Statistics.Services.Statistics;
    using MassTransit;    

    public class ReviewCreatedConsumer : IConsumer<ReviewCreatedMessage>
    {
        private readonly IStatisticsService statistics;

        public ReviewCreatedConsumer(IStatisticsService statistics)
            => this.statistics = statistics;

        public async Task Consume(ConsumeContext<ReviewCreatedMessage> context)
            => await this.statistics.AddReview();
    }
}