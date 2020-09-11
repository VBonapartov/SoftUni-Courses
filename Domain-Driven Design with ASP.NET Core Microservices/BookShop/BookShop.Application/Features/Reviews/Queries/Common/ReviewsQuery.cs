namespace BookShop.Application.Features.Reviews.Queries.Common
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using BookShop.Domain.Models.Reviews;
    using BookShop.Domain.Specifications;
    using BookShop.Domain.Specifications.Reviews;

    public abstract class ReviewsQuery
    {
        private const int ReviewsPerPage = 10;

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? SortBy { get; set; }

        public string? Order { get; set; }

        public int Page { get; set; } = 1;

        public abstract class ReviewsQueryHandler
        {
            private readonly IReviewRepository reviewRepository;

            protected ReviewsQueryHandler(IReviewRepository reviewRepository)
                => this.reviewRepository = reviewRepository;

            protected async Task<IEnumerable<TOutputModel>> GetReviewListings<TOutputModel>(
                ReviewsQuery request,
                int? reviewId = default,
                CancellationToken cancellationToken = default)
            {
                var reviewSpecification = this.GetReviewSpecification(request, reviewId);

                var searchOrder = new ReviewsSortOrder(request.SortBy, request.Order);

                var skip = (request.Page - 1) * ReviewsPerPage;

                return await this.reviewRepository.GetReviewListings<TOutputModel>(
                    reviewSpecification,
                    searchOrder,
                    skip,
                    take: ReviewsPerPage,
                    cancellationToken);
            }

            protected async Task<int> GetTotalPages(
                ReviewsQuery request,
                int? reviewId = default,
                CancellationToken cancellationToken = default)
            {
                var reviewSpecification = this.GetReviewSpecification(request, reviewId);

                var totalReviews = await this.reviewRepository.Total(
                    reviewSpecification,
                    cancellationToken);

                return (int)Math.Ceiling((double)totalReviews / ReviewsPerPage);
            }

            private Specification<Review> GetReviewSpecification(ReviewsQuery request, int? reviewId)
                => new ReviewByIdSpecification(reviewId)
                    .And(new ReviewByTitleSpecification(request.Title));
        }
    }
}