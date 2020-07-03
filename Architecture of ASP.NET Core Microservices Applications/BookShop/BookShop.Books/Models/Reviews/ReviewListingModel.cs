namespace BookShop.Books.Models.Reviews
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using BookShop.Books.Services.Models.Reviews;
    using BookShop.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using static Data.DataConstants.Review;

    public class ReviewListingModel : IMapFrom<ReviewListingServiceModel>
    {        
        public int Id { get; set; }

        [Required]
        [MinLength(MinTitleLength)]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(MinDescriptionLength)]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        [Required]
        [DisplayName("Book")]
        public int BookId { get; set; }

        [MinLength(MinBookNameLength)]
        [MaxLength(MaxBookNameLength)]
        public string BookName { get; set; }

        public List<SelectListItem> Books { get; set; }
    }
}