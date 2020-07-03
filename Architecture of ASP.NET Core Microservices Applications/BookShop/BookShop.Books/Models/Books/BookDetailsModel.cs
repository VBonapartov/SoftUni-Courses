namespace BookShop.Books.Models.Books
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using BookShop.Books.Services.Models.Books;
    using BookShop.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using static Data.DataConstants.Book;

    public class BookDetailsModel : IMapFrom<BookDetailsServiceModel>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(MinTitleLength)]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [DisplayName("Author")]
        public int AuthorId { get; set; }

        public string Author { get; set; }

        public List<SelectListItem> Authors { get; set; }

        [Required]
        [MinLength(MinDescriptionLength)]
        [MaxLength(MaxDescriptionLength)]
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

        public List<SelectListItem> CategoryList { get; set; }
    }
}