namespace BookShop.Reviews.Models.Reviews
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Review;

    public class EditReviewInputModel
    {
        [Required]
        [MinLength(MinTitleLength)]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(MinDescriptionLength)]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        [MinLength(MinAuthorLength)]
        [MaxLength(MaxAuthorLength)]
        public string Author { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        [MinLength(MinBookNameLength)]
        [MaxLength(MaxBookNameLength)]
        public string BookName { get; set; }
    }
}