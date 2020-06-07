namespace BookShop.Models.Books
{
    using BookShop.Data.Models;
    using BookShop.Infrastructure.Mapping;

    public class BookListingModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}