namespace BookShop.Domain.Reviews.Models.Reviews
{
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;
    using Common.Models;      

    public class ReviewFakes
    {
        public static class Data
        {
            public static IEnumerable<Review> GetReviews(int count = 10)
                => Enumerable
                    .Range(1, count)
                    .Select(i => GetReview(i))
                    .ToList();

            public static Review GetReview(int id = 1)
                => new Faker<Review>()
                    .CustomInstantiator(f => new Review(
                        f.Random.Number(1, 99).ToString(),
                        f.Random.Number(1, 99),
                        //f.Lorem.Letter(10),
                        $"Title{id}",
                        f.Lorem.Letter(100)))
                    .Generate()
                    .SetId(id);
        }
    }
}