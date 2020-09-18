namespace BookShop.Infrastructure.Books
{    
    using Common.Persistence;
    using Domain.Books.Models.Authors;
    using Domain.Books.Models.Books;
    using Identity;
    using Microsoft.EntityFrameworkCore;

    public interface IBooksDbContext : IDbContext
    {
        DbSet<Book> Books { get; }

        DbSet<Publisher> Publishers { get; }

        DbSet<Author> Authors { get; }

        DbSet<User> Users { get; } // TODO: Temporary workaround
    }
}
