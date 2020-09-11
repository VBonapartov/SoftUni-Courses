namespace BookShop.Application.Features.Reviews.Queries.Authors
{
    using BookShop.Application.Mapping;
    using BookShop.Domain.Models.Reviews;

    public class GetReviewAuthorOutputModel : IMapFrom<Review>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public int TotalReviews { get; set; }
    }
}