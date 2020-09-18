namespace BookShop.Domain.Reviews.Models
{    
    using Common;
    using Common.Models;
    using Exceptions;

    using static Common.Models.ModelConstants.Review;

    public class Review : Entity<int>, IAggregateRoot
    {
        internal Review(string userId, string title, string description)
        {
            this.ValidateTitle(title);
            this.ValidateDescription(description);

            this.UserId = userId;
            this.Title = title;
            this.Description = description;
        }

        private Review(string userId, string title)
        {
            this.UserId = userId;
            this.Title = title;
            this.Description = default!;
        }

        public string UserId { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public Review UpdateAuthor(string userId)
        {
            this.UserId = userId;

            return this;
        }

        public Review UpdateTitle(string title)
        {
            this.ValidateTitle(title);
            this.Title = title;

            return this;
        }

        public Review UpdateDescription(string description)
        {
            this.ValidateDescription(description);
            this.Description = description;

            return this;
        }

        private void ValidateTitle(string title)
            => Guard.ForStringLength<InvalidReviewException>(
                title,
                MinTitleLength,
                MaxTitleLength,
                nameof(this.Title));

        private void ValidateDescription(string description)
            => Guard.ForStringLength<InvalidReviewException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Title));
    }
}