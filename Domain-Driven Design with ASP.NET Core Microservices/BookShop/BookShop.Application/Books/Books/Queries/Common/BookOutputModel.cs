namespace BookShop.Application.Books.Books.Queries.Common
{
    using AutoMapper;
    using Application.Common.Mapping;
    using Domain.Books.Models.Books;

    public class BookOutputModel : IMapFrom<Book>
    {
        public int Id { get; private set; }

        public string Title { get; private set; } = default!;

        public string Publisher { get; private set; } = default!;

        public decimal Price { get; private set; }

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Book, BookOutputModel>()
                .ForMember(ad => ad.Publisher, cfg => cfg
                    .MapFrom(ad => ad.Publisher.Name));
    }
}