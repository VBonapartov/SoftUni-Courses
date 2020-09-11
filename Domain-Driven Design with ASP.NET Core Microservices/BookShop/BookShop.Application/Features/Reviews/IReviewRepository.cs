namespace BookShop.Application.Features.Reviews
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using BookShop.Application.Contracts;
    using BookShop.Application.Features.Reviews.Queries.Authors;
    using BookShop.Application.Features.Reviews.Queries.Common;
    using BookShop.Domain.Models.Reviews;
    using BookShop.Domain.Specifications;

    public interface IReviewRepository : IRepository<Review>
    {
        Task<Review> Find(int id, CancellationToken cancellationToken = default);

        Task<bool> Delete(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<TOutputModel>> GetReviewListings<TOutputModel>(
            Specification<Review> reviewSpecification,
            ReviewsSortOrder reviewsSortOrder,
            int skip = 0,
            int take = int.MaxValue,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<GetReviewAuthorOutputModel>> GetReviewAuthors(
            CancellationToken cancellationToken = default);

        Task<int> Total(
            Specification<Review> reviewSpecification,
            CancellationToken cancellationToken = default);
    }
}