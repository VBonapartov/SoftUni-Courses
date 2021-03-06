﻿namespace BookShop.Books.Services.Models.ReviewsGateway
{
    public class MineReviewOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public int BookId { get; set; }

        public string BookName { get; set; }

        public int TotalViews { get; set; }
    }
}