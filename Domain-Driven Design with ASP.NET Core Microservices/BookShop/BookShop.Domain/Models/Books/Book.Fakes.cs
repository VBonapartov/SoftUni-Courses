namespace BookShop.Domain.Models.Books
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;
    using Common;
    using FakeItEasy;

    using static Reviews.ReviewFakes.Data;

    public class BookDummyFactory : IDummyFactory
    {
        public bool CanCreate(Type type) => type == typeof(Book);

        public object? Create(Type type) => Data.GetBook();

        public Priority Priority => Priority.Default;
    }

    public static class Data
    {
        public static IEnumerable<Book> GetBooks(int count = 10)
            => Enumerable
                .Range(1, count)
                .Select(i => GetBook(i))
                .Concat(Enumerable
                    .Range(count + 1, count * 2)
                    .Select(i => GetBook(i, false)))
                .ToList();

        public static Book GetBook(int id = 1, bool isAvailable = true, int totalReviews = 10)
        {
            var book = new Faker<Book>()
                .CustomInstantiator(f => new Book(
                    f.Lorem.Letter(10),
                    new Author($"Author{id}"),
                    A.Dummy<Publisher>(),
                    f.Random.Number(100, 200),
                    A.Dummy<Options>(),
                    isAvailable))
                .Generate()
                .SetId(id);

            foreach (var review in GetReviews().Take(totalReviews))
            {
                book.AddReview(review);
            }

            return book;
        }
    }
}