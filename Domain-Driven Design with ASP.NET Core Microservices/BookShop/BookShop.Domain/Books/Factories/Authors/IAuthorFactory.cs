namespace BookShop.Domain.Books.Factories.Authors
{
    using BookShop.Domain.Common;
    using Models.Authors;

    public interface IAuthorFactory : IFactory<Author>
    {
        IAuthorFactory WithName(string name);
    }
}