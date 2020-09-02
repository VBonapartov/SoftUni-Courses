namespace BookShop.Domain.Models.Books
{
    using System;
    using FakeItEasy;

    public class AuthorDummyFactory : IDummyFactory
    {
        public bool CanCreate(Type type) => type == typeof(Author);

        public object? Create(Type type)
            => new Author("Author");

        public Priority Priority => Priority.Default;
    }
}