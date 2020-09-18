namespace BookShop.Infrastructure.Books.Configurations
{
    using Domain.Books.Models.Authors;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Domain.Common.Models.ModelConstants.Common;

    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder
                .HasMany(d => d.Books)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("books");
        }
    }
}