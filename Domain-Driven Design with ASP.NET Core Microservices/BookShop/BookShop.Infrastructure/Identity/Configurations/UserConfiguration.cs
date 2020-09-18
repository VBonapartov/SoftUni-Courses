namespace BookShop.Infrastructure.Identity.Configurations
{
    using Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(u => u.Author)
                .WithOne()
                .HasForeignKey<User>("AuthorId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}