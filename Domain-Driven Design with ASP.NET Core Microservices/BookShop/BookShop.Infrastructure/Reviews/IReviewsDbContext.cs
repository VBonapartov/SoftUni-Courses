namespace BookShop.Infrastructure.Reviews
{   
    using Domain.Reviews.Models;
    using Infrastructure.Common.Persistence;
    using Microsoft.EntityFrameworkCore;

    public interface IReviewsDbContext : IDbContext
    {
        DbSet<Review> Reviews { get; }
    }
}
