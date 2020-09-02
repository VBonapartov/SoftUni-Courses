namespace BookShop.Domain.Models.Books
{
    using System;
    using FakeItEasy;

    public class PublisherDummyFactory : IDummyFactory
    {
        public bool CanCreate(Type type) => type == typeof(Publisher);

        public object? Create(Type type)
            => new Publisher("Pubisher");

        public Priority Priority => Priority.Default;
    }
}