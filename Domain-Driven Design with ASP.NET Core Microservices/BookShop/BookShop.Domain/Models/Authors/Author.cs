namespace BookShop.Domain.Models.Authors
{
    using System.Collections.Generic;
    using System.Linq;
    using Books;
    using Common;
    using Exceptions;
    
    using static ModelConstants.Common;

    public class Author : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<Book> books;

        internal Author(string name)
        {
            this.Validate(name);

            this.Name = name;
            this.books = new HashSet<Book>();
        }

        // Необходим конструктор за EF Core
        private Author(string name, string phoneNumber = "")
        {
            this.Name = name;
            this.books = new HashSet<Book>();
        }

        public string Name { get; private set; }

        public Author UpdateName(string name)
        {
            this.Validate(name);
            this.Name = name;

            return this;
        }

        public IReadOnlyCollection<Book> Books => this.books.ToList().AsReadOnly();

        public void AddBook(Book book) => this.books.Add(book);

        private void Validate(string newName)
            => Guard.ForStringLength<InvalidAuthorException>(
                newName,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));
    }
}