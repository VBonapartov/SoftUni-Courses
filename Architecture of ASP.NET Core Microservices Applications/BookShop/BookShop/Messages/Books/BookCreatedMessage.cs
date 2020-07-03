namespace BookShop.Messages.Books
{
    public class BookCreatedMessage
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }
    }
}