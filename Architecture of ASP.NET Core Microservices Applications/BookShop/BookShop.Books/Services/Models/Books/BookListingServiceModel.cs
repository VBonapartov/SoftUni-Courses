namespace BookShop.Books.Services.Models.Books
{
    using BookShop.Books.Data.Models;
    using BookShop.Models;

    public class BookListingServiceModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}