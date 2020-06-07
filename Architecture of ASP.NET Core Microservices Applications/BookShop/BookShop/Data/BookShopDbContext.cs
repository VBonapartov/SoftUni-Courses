namespace BookShop.Data
{
    using BookShop.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class BookShopDbContext : IdentityDbContext<IdentityUser>
    {
        public BookShopDbContext(DbContextOptions<BookShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryBook> CategoryBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<CategoryBook>()
                .HasKey(cb => new { cb.CategoryId, cb.BookId });

            builder
                .Entity<CategoryBook>()
                .HasOne(cb => cb.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(cb => cb.CategoryId);

            builder
                .Entity<CategoryBook>()
                .HasOne(cb => cb.Book)
                .WithMany(b => b.Categories)
                .HasForeignKey(cb => cb.BookId);

            builder
                .Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
        }
    }
}