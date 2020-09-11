namespace BookShop.Domain.Factories.Reviews
{
    using Models.Reviews;

    internal class ReviewFactory : IReviewFactory
    {
        private string reviewTitle = default!;
        private string reviewDescription = default!;

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

        public Review Build() => new Review(this.reviewTitle, this.reviewDescription);

        public Review Build(string title, string description)
            => this
                .WithTitle(title)
                .WithDescription(description)
                .Build();
    }
}