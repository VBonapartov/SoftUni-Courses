namespace BookShop.Domain.Books.Factories.Books
{
    using BookShop.Domain.Common;
    using Models.Books;

    public interface IBookFactory : IFactory<Book>
    {
        IBookFactory WithTitle(string title);

        IBookFactory WithPublisher(string name);

        IBookFactory WithPublisher(Publisher publisher);

        IBookFactory WithPrice(decimal price);

        IBookFactory WithOptions(
            int numberOfPages,
            CoverType coverType,
            CategoryType categoryType);
    }
}