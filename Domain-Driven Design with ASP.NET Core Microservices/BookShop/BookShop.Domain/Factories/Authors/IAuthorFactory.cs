namespace BookShop.Domain.Factories.Authors
{
    using Models.Authors;

    public interface IAuthorFactory : IFactory<Author>
    {
        IAuthorFactory WithName(string name);
    }
}