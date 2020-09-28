namespace BookShop.Application.Reviews.Queries.Common
{
    using Application.Common.Mapping;
    using Domain.Reviews.Models;    

    public class ReviewOutputModel : IMapFrom<Review>
    {
        public int Id { get; private set; }

        public string BookId { get; private set; } = default!;

        public string Title { get; private set; } = default!;

        public string Description { get; private set; } = default!;
    }
}