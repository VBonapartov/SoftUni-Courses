﻿namespace BookShop.Reviews.Models.Reviews
{
    using BookShop.Models;
    using BookShop.Reviews.Data.Models;

    public class ReviewOutputModel : IMapFrom<Review>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public int BookId { get; set; }

        public string BookName { get; set; }
    }
}