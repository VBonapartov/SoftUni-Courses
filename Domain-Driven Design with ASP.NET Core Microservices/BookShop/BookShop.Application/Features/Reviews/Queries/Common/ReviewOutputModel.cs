namespace BookShop.Application.Features.Reviews.Queries.Common
{
    using BookShop.Application.Mapping;
    using BookShop.Domain.Models.Reviews;

    public class ReviewOutputModel : IMapFrom<Review>
    {
        public int Id { get; private set; }

        public string Title { get; private set; } = default!;

        public string Description { get; private set; } = default!;
    }
}