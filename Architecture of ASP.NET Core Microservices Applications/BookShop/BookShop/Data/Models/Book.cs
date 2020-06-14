namespace BookShop.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public int? Edition { get; set; } = 1;

        public int? AgeRestriction { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<CategoryBook> Categories { get; set; } = new List<CategoryBook>();
    }
}