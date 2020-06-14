namespace BookShop.Services.Models.Authors
{
    using AutoMapper;
    using BookShop.Data.Models;
    using BookShop.Infrastructure.Mapping;

    public class AuthorListingServiceModel : IMapFrom<Author>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Author, AuthorListingServiceModel>()
                .ForMember(a => a.Author, cfg => cfg
                    .MapFrom(a => $"{a.FirstName} {a.LastName}"));
        }
    }
}