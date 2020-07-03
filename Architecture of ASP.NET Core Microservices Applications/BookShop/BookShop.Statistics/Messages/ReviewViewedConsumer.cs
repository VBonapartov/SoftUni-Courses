namespace BookShop.Statistics.Messages
{
    using System.Threading.Tasks;
    using BookShop.Messages.Reviews;
    using BookShop.Statistics.Services.ReviewViews;
    using MassTransit;    

    public class ReviewViewedConsumer : IConsumer<ReviewViewedMessage>
    {
        private readonly IReviewViewService reviewView;

        public ReviewViewedConsumer(IReviewViewService reviewView)
            => this.reviewView = reviewView;

        public async Task Consume(ConsumeContext<ReviewViewedMessage> context)
            => await this.reviewView.ViewReview(
                context.Message.ReviewId,
                context.Message.UserId);
    }
}
