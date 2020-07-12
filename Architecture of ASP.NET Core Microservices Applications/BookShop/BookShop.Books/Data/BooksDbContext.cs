namespace BookShop.Books.Data
{
    using System.Reflection;
    using BookShop.Books.Data.Models;
    using BookShop.Data;
    using Microsoft.EntityFrameworkCore;
    
    public class BooksDbContext : MessageDbContext
    {
        public BooksDbContext(DbContextOptions<BooksDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryBook> CategoryBooks { get; set; }

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
    }
}