namespace BookShop.Application.Books.Authors.Queries.Details
{
    using AutoMapper;
    using Common;
    using Domain.Books.Models.Authors;

    public class AuthorDetailsOutputModel : AuthorOutputModel
    {
        public int TotalBooks { get; private set; }

        public override void Mapping(Profile mapper)
            => mapper
                .CreateMap<Author, AuthorDetailsOutputModel>()
                .IncludeBase<Author, AuthorOutputModel>()
                .ForMember(d => d.TotalBooks, cfg => cfg
                    .MapFrom(d => d.Books.Count));
    }
}