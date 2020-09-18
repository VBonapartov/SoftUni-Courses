namespace BookShop.Domain.Books.Models.Books
{
    using Common;
    using Common.Models;
    using Exceptions;    

    using static Common.Models.ModelConstants.Common;
    using static Common.Models.ModelConstants.Book;    

    public class Book : Entity<int>, IAggregateRoot
    {
        //private readonly HashSet<Review> reviews;

        internal Book(
            string title,
            Publisher publisher,                        
            decimal price,
            Options options,
            bool isAvailable)
        {
            this.Validate(title, price);

            this.Title = title;
            this.Publisher = publisher;
            this.Price = price;
            this.Options = options;
            this.IsAvailable = isAvailable;

            //this.reviews = new HashSet<Review>();
        }

        // Необходим конструктор заради EF Core
        private Book(
            string title,
            decimal price,
            bool isAvailable)
        {
            this.Title = title;
            this.Price = price;
            this.IsAvailable = isAvailable;

            this.Publisher = default!;
            this.Options = default!;

            //this.reviews = new HashSet<Review>();
        }

        public string Title { get; private set; }

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