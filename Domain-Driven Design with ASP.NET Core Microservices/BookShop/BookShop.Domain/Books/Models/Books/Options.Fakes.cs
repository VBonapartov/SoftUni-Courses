namespace BookShop.Domain.Books.Models.Books
{
    using System;
    using FakeItEasy;

    public class OptionsDummyFactory : IDummyFactory
    {
        public bool CanCreate(Type type) => type == typeof(Options);

        public object? Create(Type type)
            => new Options(100, CoverType.Hardcover, CategoryType.Horror);

        public Priority Priority => Priority.Default;
    }
}