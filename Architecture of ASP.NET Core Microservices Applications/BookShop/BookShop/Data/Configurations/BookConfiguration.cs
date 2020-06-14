namespace BookShop.Data.Configurations
{
    using BookShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static DataConstants.Book;

    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(MaxTitleLength);

            builder
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(MaxDescriptionLength);

            builder
                .Property(c => c.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder
                .Property(c => c.Copies)
                .IsRequired();

            builder
                .Property(c => c.Edition);

            builder
                .Property(c => c.AgeRestriction);

            builder
                .Property(c => c.ReleaseDate)
                .IsRequired()
                .HasColumnType("datetime2");

            builder
                .HasOne(c => c.Author)
                .WithMany(c => c.Books)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}