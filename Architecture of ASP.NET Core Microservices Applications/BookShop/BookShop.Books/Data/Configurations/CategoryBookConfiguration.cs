namespace BookShop.Books.Data.Configurations
{
    using BookShop.Books.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class CategoryBookConfiguration : IEntityTypeConfiguration<CategoryBook>
    {
        public void Configure(EntityTypeBuilder<CategoryBook> builder)
        {
            builder
                .HasKey(bc => new { bc.BookId, bc.CategoryId });
            
            builder
                .HasOne(bc => bc.Book)
                .WithMany(b => b.Categories)
                .HasForeignKey(bc => bc.BookId);
            
            builder
                .HasOne(bc => bc.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(bc => bc.CategoryId);
        }
    }
}