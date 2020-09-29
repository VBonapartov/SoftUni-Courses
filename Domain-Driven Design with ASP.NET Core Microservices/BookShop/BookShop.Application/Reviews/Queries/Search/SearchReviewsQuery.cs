namespace BookShop.Application.Reviews.Queries.Search
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using MediatR;

    public class SearchReviewsQuery : ReviewsQuery, IRequest<SearchReviewsOutputModel>
    {
        public class SearchReviewsQueryHandler : ReviewsQueryHandler, IRequestHandler<
            SearchReviewsQuery,
            SearchReviewsOutputModel>
        {
            public SearchReviewsQueryHandler(IReviewQueryRepository reviewRepository)
                : base(reviewRepository)
            {
            }

            public async Task<SearchReviewsOutputModel> Handle(
                SearchReviewsQuery request,
                CancellationToken cancellationToken)
            {
                var reviewListings = await base.GetReviewListings<ReviewOutputModel>(
                    request,
                    cancellationToken: cancellationToken);

                var totalPages = await base.GetTotalPages(
                    request,
                    cancellationToken: cancellationToken);

                return new SearchReviewsOutputModel(reviewListings, request.Page, totalPages);
            }
        }
    }
}