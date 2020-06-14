namespace BookShop.Models.Books
{
    using BookShop.Infrastructure.Mapping;
    using BookShop.Services.Models.Books;

    public class BookListingModel : IMapFrom<BookListingServiceModel>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}