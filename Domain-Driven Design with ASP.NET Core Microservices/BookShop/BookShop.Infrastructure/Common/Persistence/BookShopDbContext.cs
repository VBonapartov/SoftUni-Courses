namespace BookShop.Infrastructure.Common.Persistence
{
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Common.Models;    
    using Domain.Books.Models.Authors;
    using Domain.Books.Models.Books;
    using Domain.Reviews.Models;
    using Events;  
    using Identity;
    using Infrastructure.Books;
    using Infrastructure.Reviews;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore; 

    internal class BookShopDbContext : IdentityDbContext<User>,
        IBooksDbContext,
        IReviewsDbContext
    {
        private readonly IEventDispatcher eventDispatcher;
        private bool eventsDispatched;

        public BookShopDbContext(
            DbContextOptions<BookShopDbContext> options,
            IEventDispatcher eventDispatcher
            )
            : base(options)
        {
            this.eventDispatcher = eventDispatcher;

            this.eventsDispatched = false;
        }

        public DbSet<Book> Books { get; set; } = default!;

        public DbSet<Publisher> Publishers { get; set; } = default!;

        public DbSet<Author> Authors { get; set; } = default!;

        public DbSet<Review> Reviews { get; set; } = default!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entriesModified = 0;

            if (!this.eventsDispatched)
            {
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

                this.eventsDispatched = true;

                entriesModified = await base.SaveChangesAsync(cancellationToken);
            }

            return entriesModified;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}