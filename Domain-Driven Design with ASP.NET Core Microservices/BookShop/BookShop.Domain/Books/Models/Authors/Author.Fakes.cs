namespace BookShop.Domain.Books.Models.Authors
{
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;
    using Common.Models;

    using static Books.BookFakes.Data;

    public class AuthorFakes
    {
        public static class Data
        {
            public static IEnumerable<Author> GetAuthors(int count = 10)
                => Enumerable
                    .Range(1, count)
                    .Select(GetAuthor)
                    .ToList();

            public static Author GetAuthor(int id = 1, int totalBooks = 10)
            {
                var author = new Faker<Author>()
                    .CustomInstantiator(f => new Author(
                        $"Author{id}"))
                    .Generate()
                    .SetId(id);

                foreach (var book in GetBooks().Take(totalBooks))
                {
                    author.AddBook(book);
                }

                return author;
            }
        }
    }
}