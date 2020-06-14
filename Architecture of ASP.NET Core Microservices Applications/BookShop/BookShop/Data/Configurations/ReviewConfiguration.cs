namespace BookShop.Data.Configurations
{
    using BookShop.Data.Models;
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
                .Property(c => c.Author)
                .IsRequired()
                .HasMaxLength(MaxAuthorLength);


            builder
                .HasOne(c => c.Book)
                .WithMany(c => c.Reviews)
                .HasForeignKey(c => c.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}