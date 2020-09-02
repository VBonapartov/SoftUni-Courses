namespace BookShop.Domain.Models.Reviews
{
    using Common;
    using Exceptions;

    using static ModelConstants.Review;

    public class Review : Entity<int>, IAggregateRoot
    {
        internal Review(string title, string description)
        {
            this.ValidateTitle(title);
            this.ValidateDescription(description);

            this.Title = title;
            this.Description = description;
        }

        private Review(string title)
        {
            this.Title = title;
            this.Description = default!;
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

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