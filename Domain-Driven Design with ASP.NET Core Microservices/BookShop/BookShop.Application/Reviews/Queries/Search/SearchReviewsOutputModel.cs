namespace BookShop.Application.Reviews.Queries.Search
{
    using System.Collections.Generic;
    using Queries.Common;

    public class SearchReviewsOutputModel : ReviewsOutputModel<ReviewOutputModel>
    {
        public SearchReviewsOutputModel(
            IEnumerable<ReviewOutputModel> reviews,
            int page,
            int totalPages)
            : base(reviews, page, totalPages)
        {
        }
    }
}