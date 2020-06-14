namespace BookShop.Services.Models.Books
{
    using BookShop.Data.Models;
    using BookShop.Infrastructure.Mapping;

    public class BookListingServiceModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}