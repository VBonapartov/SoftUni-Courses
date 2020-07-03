namespace BookShop.Statistics.Data.Configurations
{
    using BookShop.Statistics.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class BookViewConfiguration : IEntityTypeConfiguration<BookView>
    {
        public void Configure(EntityTypeBuilder<BookView> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .HasIndex(c => c.BookId);

            builder
                .Property(c => c.UserId)
                .IsRequired();
        }
    }
}