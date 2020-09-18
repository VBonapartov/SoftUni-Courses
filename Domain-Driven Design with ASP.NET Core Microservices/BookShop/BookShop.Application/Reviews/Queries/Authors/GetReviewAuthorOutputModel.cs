namespace BookShop.Application.Reviews.Queries.Authors
{
    using Application.Common.Mapping;
    using Domain.Reviews.Models;    

    public class GetReviewAuthorOutputModel : IMapFrom<Review>
    {
        public int Id { get; private set; }

        public string UserId { get; private set; } = default!;

        //public string Name { get; private set; } = default!;

        public int TotalReviews { get; set; }
    }
}