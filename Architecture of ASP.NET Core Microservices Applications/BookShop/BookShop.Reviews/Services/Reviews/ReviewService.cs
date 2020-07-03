namespace BookShop.Reviews.Services.Reviews
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookShop.Messages.Reviews;
    using BookShop.Reviews.Data;
    using BookShop.Reviews.Data.Models;
    using BookShop.Reviews.Models.Reviews;
    using BookShop.Services.Identity;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;

    public class ReviewService : IReviewService
    {
        private readonly ReviewsDbContext db;

        private readonly ICurrentUserService user;

        private readonly IMapper mapper;

        private readonly IBus publisher;

        public ReviewService(ReviewsDbContext db,
             ICurrentUserService user, 
             IMapper mapper, 
             IBus publisher)
        {
            this.db = db;
            this.user = user;
            this.mapper = mapper;
            this.publisher = publisher;
        }

        public async Task<IEnumerable<ReviewOutputModel>> All()
            => await this.db
                .Reviews
                .AsNoTracking()
                .ProjectTo<ReviewOutputModel>(mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<ReviewOutputModel> Details(int id)
        { 
            var details = await this.db
                .Reviews
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ProjectTo<ReviewOutputModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            await this.publisher.Publish(new ReviewViewedMessage
            {
                ReviewId = id,
                UserId = string.IsNullOrEmpty(this.user.UserId)
                        ? string.Empty
                        : this.user.UserId
            }); 

            return details;
        }

        public async Task<IEnumerable<ReviewOutputModel>> Mine(string authorId)
            => await this.db
                .Reviews
                .AsNoTracking()
                .Where(r => r.AuthorId == authorId)
                .ProjectTo<ReviewOutputModel>(mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<bool> Exists(int id)
            => await this.db.Reviews.AnyAsync(b => b.Id == id);

        public async Task Create(
                                 string title,
                                 string description,
                                 string authorId,
                                 string author,
                                 int bookId,
                                 string bookName)
        {
            var review = new Review
            {
                Title = title,
                Description = description,
                AuthorId = authorId,
                Author = author,
                BookId = bookId,
                BookName = bookName
            };

            await this.db.Reviews.AddAsync(review);
            await this.db.SaveChangesAsync();

            await this.publisher.Publish(new ReviewCreatedMessage
            {
                ReviewId = review.Id
            });
        }

        public async Task Update(
            int id,
            string title,
            string description,
            string authorId,
            string author,
            int bookId,
            string bookName)
        {
            var review = await this.db.Reviews.FindAsync(id);
            if (review == null)
            {
                return;
            }

            review.Title = title;
            review.Description = description;
            review.AuthorId = authorId;
            review.Author = author;
            review.BookId = bookId;
            review.BookName = bookName;

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