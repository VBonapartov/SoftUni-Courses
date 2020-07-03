namespace BookShop.Books.Models.Authors
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BookShop.Books.Services.Models.Authors;
    using BookShop.Models;

    using static Data.DataConstants.Author;

    public class AuthorDetailsModel : IMapFrom<AuthorDetailsServiceModel>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(MinFirstNameLength)]
        [MaxLength(MaxFirstNameLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(MinLastNameLength)]
        [MaxLength(MaxLastNameLength)]
        public string LastName { get; set; }

        public IEnumerable<string> Books { get; set; }
    }
}