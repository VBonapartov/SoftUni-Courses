namespace BookShop.Infrastructure.Statistics.Configuration
{
    using Domain.Statistics.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StatisticsConfiguration : IEntityTypeConfiguration<Statistics>
    {
        public void Configure(EntityTypeBuilder<Statistics> builder)
        {
            const string id = "Id";

            builder
                .Property<int>(id);

            builder
                .HasKey(id);

            builder
                .HasMany(d => d.BookViews)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("bookViews");
        }
    }
}
