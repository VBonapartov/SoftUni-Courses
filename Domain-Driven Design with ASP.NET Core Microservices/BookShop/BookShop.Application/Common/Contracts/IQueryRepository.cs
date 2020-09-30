namespace BookShop.Application.Common.Contracts
{
    using BookShop.Domain.Common;

    public interface IQueryRepository<in TEntity>
        where TEntity : IAggregateRoot
    {
    }
}
