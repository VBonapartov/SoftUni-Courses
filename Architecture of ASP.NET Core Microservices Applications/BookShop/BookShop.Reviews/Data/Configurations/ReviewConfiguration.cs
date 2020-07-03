namespace BookShop.Reviews.Data.Configurations
{
    using BookShop.Reviews.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static DataConstants.Review;

    internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
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
                .Property(c => c.AuthorId)
                .IsRequired();

            builder
                .Property(c => c.Author)
                .IsRequired()
                .HasMaxLength(MaxAuthorLength);

            builder
                .Property(c => c.BookId)
                .IsRequired();

            builder
                .Property(c => c.BookName)
                .IsRequired()
                .HasMaxLength(MaxBookNameLength);
        }
    }
}