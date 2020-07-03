namespace BookShop.Notifications.Messages
{
    using System.Threading.Tasks;
    using BookShop.Messages.Books;
    using BookShop.Notifications.Hub;
    using MassTransit;
    using Microsoft.AspNetCore.SignalR;

    using static Constants;

    public class BookCreatedConsumer : IConsumer<BookCreatedMessage>
    {
        private readonly IHubContext<NotificationsHub> hub;

        public BookCreatedConsumer(IHubContext<NotificationsHub> hub)
            => this.hub = hub;

        public async Task Consume(ConsumeContext<BookCreatedMessage> context)
            => await this.hub
                .Clients
                .Groups(AuthenticatedUsersGroup)
                .SendAsync(ReceiveNotificationEndpoint, context.Message);
    }
}
