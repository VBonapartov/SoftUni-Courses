namespace BookShop.Statistics.Data.Configurations
{
    using BookShop.Statistics.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class StatisticsConfiguration : IEntityTypeConfiguration<Statistics>
    {
        public void Configure(EntityTypeBuilder<Statistics> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.TotalBooks)
                .IsRequired();

            builder
                .Property(c => c.TotalReviews)
                .IsRequired();
        }
    }
}