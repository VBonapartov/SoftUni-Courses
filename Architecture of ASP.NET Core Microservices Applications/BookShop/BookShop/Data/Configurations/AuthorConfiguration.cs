namespace BookShop.Data.Configurations
{
    using BookShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static DataConstants.Author;

    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(MaxFirstNameLength);

            builder
                .Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(MaxLastNameLength);
        }
    }
}