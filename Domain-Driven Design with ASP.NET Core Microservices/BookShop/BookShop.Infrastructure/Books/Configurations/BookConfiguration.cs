namespace BookShop.Infrastructure.Books.Configurations
{
    using Domain.Books.Models.Books;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Domain.Common.Models.ModelConstants.Book;

    internal class CarAdConfiguration : IEntityTypeConfiguration<Book>
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
                .HasOne(c => c.Publisher)
                .WithMany()
                .HasForeignKey("PublisherId")
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(c => c.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder
                .Property(c => c.IsAvailable)
                .IsRequired();

            builder
                .OwnsOne(c => c.Options, o =>
                {
                    o.WithOwner();

                    o.Property(op => op.NumberOfPages);

                    o.OwnsOne(
                        op => op.CoverType,
                        t =>
                        {
                            t.WithOwner();

                            t.Property(tr => tr.Value);
                        });

                    o.OwnsOne(
                        op => op.CategoryType,
                        t =>
                        {
                            t.WithOwner();

                            t.Property(tr => tr.Value);
                        });
                });
        }
    }
}