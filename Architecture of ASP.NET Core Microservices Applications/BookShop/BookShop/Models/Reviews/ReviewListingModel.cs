namespace BookShop.Models.Reviews
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ReviewListingModel
    {        
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Author { get; set; }

        public string BookName { get; set; }

        [Required]
        [DisplayName("Book")]
        public int BookId { get; set; }

        public List<SelectListItem> Books { get; set; }
    }
}