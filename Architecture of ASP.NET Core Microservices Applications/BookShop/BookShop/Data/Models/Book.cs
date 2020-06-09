namespace BookShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue)]
        public int Copies { get; set; }

        public int? Edition { get; set; } = 1;

        public int? AgeRestriction { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<CategoryBook> Categories { get; set; } = new List<CategoryBook>();
    }
}