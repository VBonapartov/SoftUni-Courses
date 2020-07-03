namespace BookShop.Messages.Books
{
    public class BookViewedMessage
    {
        public int BookId { get; set; }

        public string UserId { get; set; }
    }
}