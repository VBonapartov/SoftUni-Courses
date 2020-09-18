namespace BookShop.Domain.Reviews.Factories
{
    using Common;
    using Models;

    public interface IReviewFactory : IFactory<Review>
    {
        IReviewFactory WithAuthor(string userId);

        IReviewFactory WithTitle(string title);

        IReviewFactory WithDescription(string description);
    }
}