namespace BookShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Review
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

        [Required]
        public int BookId { get; set; }

        [Required]
        public Book Book { get; set; }
    }
}