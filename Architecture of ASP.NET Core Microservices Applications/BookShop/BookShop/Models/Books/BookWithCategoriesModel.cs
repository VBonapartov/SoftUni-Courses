namespace BookShop.Models.Books
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class BookWithCategoriesModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Copies { get; set; }

        public int? Edition { get; set; }

        public int? AgeRestriction { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [DisplayName("Categories")]
        public IEnumerable<int> CategoriesId { get; set; }

        public IEnumerable<string> Categories { get; set; }
    }
}