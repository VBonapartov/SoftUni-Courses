namespace BookShop.Domain.Reviews.Factories
{
    using Models;

    internal class ReviewFactory : IReviewFactory
    {
        private string userId = default!;
        private string reviewTitle = default!;
        private string reviewDescription = default!;

        public IReviewFactory WithAuthor(string userId)
        {
            this.userId = userId;
            return this;
        }

        public IReviewFactory WithTitle(string title)
        {
            this.reviewTitle = title;
            return this;
        }

        public IReviewFactory WithDescription(string description)
        {
            this.reviewDescription = description;
            return this;
        }

        public Review Build() => new Review(this.userId, this.reviewTitle, this.reviewDescription);

        public Review Build(string userId, string title, string description)
            => this
                .WithAuthor(userId)
                .WithTitle(title)
                .WithDescription(description)
                .Build();
    }
}