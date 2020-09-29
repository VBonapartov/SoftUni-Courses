namespace BookShop.Infrastructure.Reviews
{   
    using Domain.Reviews.Models;
    using Infrastructure.Common.Persistence;
    using Microsoft.EntityFrameworkCore;

    internal interface IReviewsDbContext : IDbContext
    {
        DbSet<Review> Reviews { get; }
    }
}
