namespace BookShop.Statistics.Messages
{
    using System.Threading.Tasks;
    using BookShop.Messages.Books;
    using BookShop.Statistics.Services.BookViews;
    using MassTransit;

    public class BookViewedConsumer : IConsumer<BookViewedMessage>
    {
        private readonly IBookViewService bookView;

        public BookViewedConsumer(IBookViewService bookView)
            => this.bookView = bookView;

        public async Task Consume(ConsumeContext<BookViewedMessage> context)
            => await this.bookView.ViewBook(
                context.Message.BookId,
                context.Message.UserId);
    }
}