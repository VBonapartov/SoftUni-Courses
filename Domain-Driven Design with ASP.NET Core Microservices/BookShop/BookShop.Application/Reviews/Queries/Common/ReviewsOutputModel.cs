namespace BookShop.Application.Reviews.Queries.Common
{
    using System.Collections.Generic;

    public abstract class ReviewsOutputModel<TReviewOutputModel>
    {
        internal ReviewsOutputModel(
            IEnumerable<TReviewOutputModel> reviews,
            int page,
            int totalPages)
        {
            this.Reviews = reviews;
            this.Page = page;
            this.TotalPages = totalPages;
        }

        public IEnumerable<TReviewOutputModel> Reviews { get; }

        public int Page { get; }

        public int TotalPages { get; }
    }
}