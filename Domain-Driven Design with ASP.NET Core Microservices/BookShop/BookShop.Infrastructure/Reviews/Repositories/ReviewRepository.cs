namespace BookShop.Infrastructure.Reviews.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Reviews;
    using Application.Reviews.Queries.Authors;
    using Application.Reviews.Queries.Common;
    using AutoMapper;   
    using Domain.Common;
    using Domain.Reviews.Models;
    using Domain.Reviews.Repositories;
    using Infrastructure.Common;
    using Infrastructure.Common.Persistence;
    using Microsoft.EntityFrameworkCore;

    internal class ReviewRepository : DataRepository<IReviewsDbContext, Review>, IReviewQueryRepository, IReviewDomainRepository
    {
        private readonly IMapper mapper;

        public ReviewRepository(BookShopDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public async Task<Review> Find(int id, CancellationToken cancellationToken = default)
            => await this
                .All()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var review = await this.Data.Reviews.FindAsync(id);

            if (review == null)
            {
                return false;
            }

            this.Data.Reviews.Remove(review);

            await this.Data.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IEnumerable<TOutputModel>> GetReviewListings<TOutputModel>(
            Specification<Review> reviewSpecification,
            ReviewsSortOrder reviewsSortOrder,
            int skip = 0,
            int take = int.MaxValue,
            CancellationToken cancellationToken = default)
            => (await this.mapper
                .ProjectTo<TOutputModel>(this
                    .GetReviewsQuery(reviewSpecification)
                    .Sort(reviewsSortOrder))
                .ToListAsync(cancellationToken))
                .Skip(skip)
                .Take(take); // EF Core bug forces me to execute paging on the client.

        public async Task<IEnumerable<GetReviewAuthorOutputModel>> GetReviewAuthors(
            CancellationToken cancellationToken = default)
        {
            var reviews = await this.mapper
                .ProjectTo<GetReviewAuthorOutputModel>(this.Data.Reviews)
                .ToDictionaryAsync(k => k.Id, cancellationToken);

            var reviewsPerAuthor = await this.Data.Reviews
                .GroupBy(r => r.UserId)
                .Select(gr => new
                {
                    UserId = gr.Key,
                    TotalReviews = gr.Count()
                })
                .ToListAsync(cancellationToken);

            reviewsPerAuthor.ForEach(r => {
                reviews
                    .Where(t => t.Value.UserId == r.UserId)
                    .ToList()
                    .ForEach(t => t.Value.TotalReviews = r.TotalReviews);
            });

            return reviews.Values;
        } 

        public async Task<int> Total(
            Specification<Review> reviewSpecification,
            CancellationToken cancellationToken = default)
            => await this
                .GetReviewsQuery(reviewSpecification)
                .CountAsync(cancellationToken);

        private IQueryable<Review> GetReviewsQuery(
            Specification<Review> reviewSpecification)
            => this
                .Data
                .Reviews
                .Where(reviewSpecification);
    }
}
