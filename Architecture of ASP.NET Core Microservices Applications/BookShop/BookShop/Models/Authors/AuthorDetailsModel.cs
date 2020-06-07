namespace BookShop.Models.Authors
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using BookShop.Data.Models;
    using BookShop.Infrastructure.Mapping;

    public class AuthorDetailsModel : IMapFrom<Author>, IHaveCustomMapping
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; }

        public IEnumerable<string> Books { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Author, AuthorDetailsModel>()
                .ForMember(a => a.Books, cfg => cfg
                    .MapFrom(a => a.Books.Select(b => b.Title)));
        }
    }
}