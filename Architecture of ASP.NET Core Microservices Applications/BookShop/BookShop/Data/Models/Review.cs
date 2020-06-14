namespace BookShop.Data.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}