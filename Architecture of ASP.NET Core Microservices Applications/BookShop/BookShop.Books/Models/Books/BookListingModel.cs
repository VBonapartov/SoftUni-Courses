namespace BookShop.Books.Models.Books
{
    using BookShop.Books.Services.Models.Books;
    using BookShop.Models;

    public class BookListingModel : IMapFrom<BookListingServiceModel>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}