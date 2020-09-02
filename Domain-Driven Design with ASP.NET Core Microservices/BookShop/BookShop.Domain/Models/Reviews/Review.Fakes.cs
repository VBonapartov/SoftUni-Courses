namespace BookShop.Domain.Models.Reviews
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Bogus;    

    public class ReviewFakes
    {
        public static class Data
        {
            public static IEnumerable<Review> GetReviews(int count = 10)
                => Enumerable
                    .Range(1, count)
                    .Select(GetReview)
                    .ToList();

            public static Review GetReview(int id = 1)
                => new Faker<Review>()
                    .CustomInstantiator(f => new Review(
                        f.Lorem.Letter(10),
                        f.Lorem.Letter(100)))
                    .Generate()
                    .SetId(id);
        }
    }
}