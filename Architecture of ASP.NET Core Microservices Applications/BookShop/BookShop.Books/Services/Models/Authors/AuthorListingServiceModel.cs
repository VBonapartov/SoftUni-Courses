namespace BookShop.Books.Services.Models.Authors
{
    using AutoMapper;
    using BookShop.Books.Data.Models;
    using BookShop.Models;

    public class AuthorListingServiceModel : IMapFrom<Author>
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public void Mapping(Profile mapper)
        {
            mapper
                .CreateMap<Author, AuthorListingServiceModel>()
                .ForMember(a => a.Author, cfg => cfg
                    .MapFrom(a => $"{a.FirstName} {a.LastName}"));
        }
    }
}