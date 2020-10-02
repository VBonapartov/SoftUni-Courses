namespace BookShop.Domain.Reviews.Factories
{
    using Models;

    internal class ReviewFactory : IReviewFactory
    {
        private int bookId = default!;
        private string userId = default!;       
        private string reviewTitle = default!;
        private string reviewDescription = default!;

        public IReviewFactory ForBook(int bookId)
        {
            this.bookId = bookId;
            return this;
        }

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

        public Review Build() => new Review(this.userId, this.bookId, this.reviewTitle, this.reviewDescription);

        public Review Build(string userId, int bookId, string title, string description)
            => this
                .ForBook(bookId)
                .WithAuthor(userId)
                .WithTitle(title)
                .WithDescription(description)
                .Build();
    }
}