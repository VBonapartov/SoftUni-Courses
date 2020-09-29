namespace BookShop.Infrastructure.Common.Persistence
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;        
    using Domain.Books.Models.Authors;
    using Domain.Books.Models.Books;
    using Domain.Common.Models;
    using Domain.Reviews.Models;
    using Domain.Statistics.Models;
    using Events;  
    using Identity;
    using Infrastructure.Books;
    using Infrastructure.Reviews;
    using Infrastructure.Statistics;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;  

    internal class BookShopDbContext : IdentityDbContext<User>,
        IBooksDbContext,
        IReviewsDbContext,
        IStatisticsDbContext
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly Stack<object> savesChangesTracker;

        public BookShopDbContext(
            DbContextOptions<BookShopDbContext> options,
            IEventDispatcher eventDispatcher
            )
            : base(options)
        {
            this.eventDispatcher = eventDispatcher;

            this.savesChangesTracker = new Stack<object>();
        }

        public DbSet<Book> Books { get; set; } = default!;

        public DbSet<Publisher> Publishers { get; set; } = default!;

        public DbSet<Author> Authors { get; set; } = default!;

        public DbSet<Review> Reviews { get; set; } = default!;

        public DbSet<Statistics> Statistics { get; set; } = default!;

        public DbSet<BookView> BookViews { get; set; } = default!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.savesChangesTracker.Push(new object());

            var entities = this.ChangeTracker
                .Entries<IEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entities)
            {
                var events = entity.Events.ToArray();

                entity.ClearEvents();

                foreach (var domainEvent in events)
                {
                    await this.eventDispatcher.Dispatch(domainEvent);
                }
            }

            this.savesChangesTracker.Pop();

            if (!this.savesChangesTracker.Any())
            {
                return await base.SaveChangesAsync(cancellationToken);
            }

            return 0;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}