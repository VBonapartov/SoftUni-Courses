namespace BookShop.Infrastructure.Reviews
{
    using Domain.Reviews.Models;
    using Microsoft.EntityFrameworkCore;

    public interface IReviewsDbContext
    {
        DbSet<Review> Reviews { get; }
    }
}
