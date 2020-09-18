namespace BookShop.Domain.Books.Factories.Books
{
    using Exceptions;
    using Models.Books;

    internal class BookFactory : IBookFactory
    {
        private string bookTitle = default!;
        private Publisher bookPublisher = default!;
        private decimal bookPrice = default!;
        private Options bookOptions = default!;

        private bool authorSet = false;
        private bool publisherSet = false;
        private bool optionsSet = false;

        public IBookFactory WithTitle(string title)
        { 
            this.bookTitle = title;
            return this;
        }

        public IBookFactory WithPublisher(string name)
            => this.WithPublisher(new Publisher(name));

        public IBookFactory WithPublisher(Publisher publisher)
        {
            this.bookPublisher = publisher;
            this.publisherSet = true;
            return this;
        }

        public IBookFactory WithPrice(decimal price)
        {
            this.bookPrice = price;
            return this;
        }

        public IBookFactory WithOptions(int numberOfPages, CoverType coverType, CategoryType categoryType)
            => this.WithOptions(new Options(numberOfPages, coverType, categoryType));

        public IBookFactory WithOptions(Options options)
        {
            this.bookOptions = options;
            this.optionsSet = true;
            return this;
        }

        public Book Build()
        {
            if (string.IsNullOrEmpty(this.bookTitle) || 
                !this.authorSet || 
                !this.publisherSet || 
                !this.optionsSet)
            {
                throw new InvalidBookException("Title, author, publisher and options must have a value.");
            }

            return new Book(
                this.bookTitle,
                this.bookPublisher,
                this.bookPrice,
                this.bookOptions,
                true);
        }
    }
}