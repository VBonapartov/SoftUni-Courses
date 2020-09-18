﻿namespace BookShop.Infrastructure.Common.Persistence
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Contracts;
    using Domain.Common;

    internal abstract class DataRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        protected DataRepository(BookShopDbContext db) => this.Data = db;

        protected BookShopDbContext Data { get; }

        protected IQueryable<TEntity> All() => this.Data.Set<TEntity>();

        public async Task Save(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            this.Data.Update(entity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}