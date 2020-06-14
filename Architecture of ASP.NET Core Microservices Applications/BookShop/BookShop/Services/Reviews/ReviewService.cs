namespace BookShop.Services.Reviews
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Services.Models.Reviews;
    using Microsoft.EntityFrameworkCore;

    public class ReviewService : IReviewService
    {
        private readonly BookShopDbContext db;

        private readonly IMapper mapper;

        public ReviewService(BookShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReviewListingServiceModel>> All()
            => await this.db
                .Reviews
                .AsNoTracking()
                .ProjectTo<ReviewListingServiceModel>(mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<ReviewListingServiceModel> Details(int id)
            => await this.db
                .Reviews
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ProjectTo<ReviewListingServiceModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<bool> Exists(int id)
            => await this.db.Reviews.AnyAsync(b => b.Id == id);

        public async Task Create(
                                 string title,
                                 string description,
                                 string author,
                                 int bookId)
        {
            var review = new Review
            {
                Title = title,
                Description = description,
                Author = author,
                BookId = bookId
            };

            await this.db.Reviews.AddAsync(review);
            await this.db.SaveChangesAsync();
        }


        public async Task Update(
            int id,
            string title,
            string description,
            string author,
            int bookId)
        {
            var review = await this.db.Reviews.FindAsync(id);
            if (review == null)
            {
                return;
            }

            review.Title = title;
            review.Description = description;
            review.Author = author;
            review.BookId = bookId;

            await this.db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var review = await this.db.Reviews.FindAsync(id);
            if (review == null)
            {
                return;
            }

            this.db.Remove(review);
            await this.db.SaveChangesAsync();
        }
    }
}
