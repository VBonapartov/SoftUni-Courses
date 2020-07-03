namespace BookShop.Books.Services.Models.Authors
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using BookShop.Books.Data.Models;
    using BookShop.Models;

    public class AuthorDetailsServiceModel : IMapFrom<Author>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<string> Books { get; set; }

        public void Mapping(Profile mapper)
        {
            mapper
                .CreateMap<Author, AuthorDetailsServiceModel>()
                .ForMember(a => a.Books, cfg => cfg
                    .MapFrom(a => a.Books.Select(b => b.Title)));
        }
    }
}