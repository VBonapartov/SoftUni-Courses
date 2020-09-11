namespace BookShop.Domain.Models.Authors
{
    using System;
    using FakeItEasy;
    using Models.Authors;

    public class AuthorDummyFactory : IDummyFactory
    {
        public bool CanCreate(Type type) => type == typeof(Author);

        public object? Create(Type type)
            => new Author("Author");

        public Priority Priority => Priority.Default;
    }
}