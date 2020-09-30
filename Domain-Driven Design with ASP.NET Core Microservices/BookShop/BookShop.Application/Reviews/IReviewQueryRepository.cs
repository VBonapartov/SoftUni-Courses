namespace BookShop.Application.Reviews
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Contracts;
    using Domain.Common;
    using Domain.Reviews.Models;
    using Queries.Authors;
    using Queries.Common;

    public interface IReviewQueryRepository : IQueryRepository<Review>
    { 
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