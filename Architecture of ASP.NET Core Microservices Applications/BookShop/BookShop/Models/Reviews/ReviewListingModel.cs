namespace BookShop.Models.Reviews
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using BookShop.Infrastructure.Mapping;
    using BookShop.Services.Models.Reviews;
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

        [Required]
        [MinLength(MinAuthorLength)]
        [MaxLength(MaxAuthorLength)]
        public string Author { get; set; }

        public string BookName { get; set; }

        [Required]
        [DisplayName("Book")]
        public int BookId { get; set; }

        public List<SelectListItem> Books { get; set; }
    }
}