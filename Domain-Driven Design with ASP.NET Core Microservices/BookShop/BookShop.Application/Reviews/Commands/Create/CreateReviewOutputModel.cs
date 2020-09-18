namespace BookShop.Application.Reviews.Commands.Create
{
    public class CreateReviewOutputModel
    {
        public CreateReviewOutputModel(int reviewId)
            => this.ReviewId = reviewId;

        public int ReviewId { get; }
    }
}