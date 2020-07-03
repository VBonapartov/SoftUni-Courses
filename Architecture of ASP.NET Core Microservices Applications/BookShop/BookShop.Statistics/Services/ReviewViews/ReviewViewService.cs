namespace BookShop.Statistics.Services.ReviewViews
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BookShop.Statistics.Data;
    using BookShop.Statistics.Data.Models;
    using BookShop.Statistics.Models.ReviewViews;
    using Microsoft.EntityFrameworkCore;

    public class ReviewViewService : IReviewViewService
    {
        private readonly StatisticsDbContext db;

        public ReviewViewService(StatisticsDbContext db)
        {
            this.db = db;
        }

        public async Task<int> GetTotalViews(int reviewId)
            => await this.db
                .ReviewView
                .CountAsync(v => v.ReviewId == reviewId);

        public async Task<IEnumerable<ReviewViewOutputModel>> GetTotalViews(
            IEnumerable<int> ids)
            => await this.db
                .ReviewView
                .Where(v => ids.Contains(v.ReviewId))
                .GroupBy(v => v.ReviewId)
                .Select(gr => new ReviewViewOutputModel
                {
                    ReviewId = gr.Key,
                    TotalViews = gr.Count()
                })
                .ToListAsync();

        public async Task ViewReview(int reviewId, string userId)
        {
            var reviewView = new ReviewView
            {
                ReviewId = reviewId,
                UserId = userId
            };
            
            await this.db.ReviewView.AddAsync(reviewView);
            await this.db.SaveChangesAsync();
        }
    }
}