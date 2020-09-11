namespace BookShop.Domain.Factories.Reviews
{
    using Models.Reviews;

    public interface IReviewFactory : IFactory<Review>
    {
        IReviewFactory WithTitle(string title);

        IReviewFactory WithDescription(string description);
    }
}