namespace BookShop.Application.Features.Books.Commands.Create
{
    public class CreateBookOutputModel
    {
        public CreateBookOutputModel(int bookId)
            => this.BookId = bookId;

        public int BookId { get; }
    }
}