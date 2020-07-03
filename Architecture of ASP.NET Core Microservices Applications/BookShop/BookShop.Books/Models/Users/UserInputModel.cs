namespace BookShop.Books.Models.Users
{
    using System.ComponentModel.DataAnnotations;   

    public class UserInputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}