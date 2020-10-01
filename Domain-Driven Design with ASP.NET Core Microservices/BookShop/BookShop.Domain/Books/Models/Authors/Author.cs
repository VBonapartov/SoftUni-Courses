namespace BookShop.Domain.Books.Models.Authors
{
    using System.Collections.Generic;
    using System.Linq;
    using Books;
    using Common;
    using Domain.Books.Events.Books;
    using Domain.Common.Models;
    using Exceptions;
    
    using static Common.Models.ModelConstants.Common;

    public class Author : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<Book> books;

        internal Author(string name, string userId)
        {
            this.Validate(name);

            this.UserId = userId;
            this.Name = name;
            this.books = new HashSet<Book>();
        }

        // Необходим конструктор за EF Core
        //private Author(string name)
        //{
        //    this.Name = name;
        //    this.books = new HashSet<Book>();
        //}

        public string UserId { get; private set; }

        public string Name { get; private set; }

        public Author UpdateName(string name)
        {
            this.Validate(name);
            this.Name = name;

            return this;
        }

        public IReadOnlyCollection<Book> Books => this.books.ToList().AsReadOnly();

        public void AddBook(Book book)
        {
            this.books.Add(book);

            this.AddEvent(new BookAddedEvent());
        }

        private void Validate(string newName)
            => Guard.ForStringLength<InvalidAuthorException>(
                newName,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));
    }
}