namespace BookShop.Application.Books.Authors.Queries.Common
{
    using AutoMapper;    
    using Application.Common.Mapping;
    using Domain.Books.Models.Authors;

    public class AuthorOutputModel : IMapFrom<Author>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Author, AuthorOutputModel>();
    }
}