namespace BookShop.Domain.Models.Books
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Exceptions;
    using Reviews;

    using static ModelConstants.Common;
    using static ModelConstants.Book;
  
    public class Book : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<Review> reviews;

        internal Book(
            string title,
            Author author,
            Publisher publisher,                        
            decimal price,
            Options options,
            bool isAvailable)
        {
            this.Validate(title, price);

            this.Title = title;
            this.Author = author;
            this.Publisher = publisher;
            this.Price = price;
            this.Options = options;
            this.IsAvailable = isAvailable;

            this.reviews = new HashSet<Review>();
        }

        private Book(
            string title,
            decimal price,
            bool isAvailable)
        {
            this.Title = title;
            this.Price = price;
            this.IsAvailable = isAvailable;

            this.Author = default!;
            this.Publisher = default!;
            this.Options = default!;

            this.reviews = new HashSet<Review>();
        }

        public string Title { get; private set; }

        public Author Author { get; private set; }

        public Publisher Publisher { get; private set; }

        public decimal Price { get; private set; }

        public Options Options { get; private set; }

        public bool IsAvailable { get; private set; }

        public Book UpdateTitle(string title)
        {
            this.ValidateTitle(title);
            this.Title = title;

            return this;
        }

        public Book UpdateAuthor(string author)
        {
            if (this.Author.Name != author)
            {
                this.Author = new Author(author);
            }

            return this;
        }

        public Book UpdatePublisher(string publisher)
        {
            if (this.Publisher.Name != publisher)
            {
                this.Publisher = new Publisher(publisher);
            }

            return this;
        }

        public Book UpdatePrice(decimal price)
        {
            this.ValidatePrice(price);
            this.Price = price;

            return this;
        }

        public Book UpdateOptions(int numberOfPages, CoverType coverType, CategoryType categoryType)
        {
            this.Options = new Options(numberOfPages, coverType, categoryType);
            
            return this;
        }

        public Book ChangeAvailability()
        {
            this.IsAvailable = !this.IsAvailable;

            return this;
        }

        public IReadOnlyCollection<Review> Reviews => this.reviews.ToList().AsReadOnly();

        public void AddReview(Review review) => this.reviews.Add(review);

        private void Validate(string title, decimal price)
        {
            this.ValidateTitle(title);
            this.ValidatePrice(price);
        }

        private void ValidateTitle(string title)
            => Guard.ForStringLength<InvalidBookException>(
                title,
                MinTitleLength,
                MaxTitleLength,
                nameof(this.Title));

        private void ValidatePrice(decimal price)
            => Guard.AgainstOutOfRange<InvalidBookException>(
                price,
                Zero,
                decimal.MaxValue,
                nameof(this.Price));
    }
}