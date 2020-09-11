namespace BookShop.Application.Features.Authors.Queries.Common
{
    using AutoMapper;
    using Domain.Models.Authors;
    using Mapping;

    public class AuthorOutputModel : IMapFrom<Author>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Author, AuthorOutputModel>();
    }
}