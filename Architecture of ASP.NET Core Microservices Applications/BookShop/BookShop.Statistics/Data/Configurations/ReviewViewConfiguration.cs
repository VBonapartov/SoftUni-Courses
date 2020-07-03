namespace BookShop.Statistics.Data.Configurations
{
    using BookShop.Statistics.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ReviewViewConfiguration : IEntityTypeConfiguration<ReviewView>
    {
        public void Configure(EntityTypeBuilder<ReviewView> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .HasIndex(c => c.ReviewId);

            builder
                .Property(c => c.UserId)
                .IsRequired();
        }
    }
}