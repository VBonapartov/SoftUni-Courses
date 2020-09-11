namespace BookShop.Application.Features.Books.Queries.Details
{
    using AutoMapper;
    using BookShop.Application.Features.Authors.Queries.Common;
    using BookShop.Application.Features.Books.Queries.Common;
    using BookShop.Domain.Common;
    using BookShop.Domain.Models.Books;

    public class BookDetailsOutputModel : BookOutputModel
    {
        public int NumberOfPages { get; private set; }

        public string CoverType { get; private set; } = default!;

        public string CategoryType { get; private set; } = default!;

        public AuthorOutputModel Author { get; set; } = default!;

        public override void Mapping(Profile mapper)
            => mapper
                .CreateMap<Book, BookDetailsOutputModel>()
                .IncludeBase<Book, BookOutputModel>()
                .ForMember(c => c.NumberOfPages, cfg => cfg
                    .MapFrom(c => c.Options.NumberOfPages))
                .ForMember(c => c.CoverType, cfg => cfg
                    .MapFrom(c => Enumeration.NameFromValue<CoverType>(
                        c.Options.CoverType.Value)))
                .ForMember(c => c.CategoryType, cfg => cfg
                    .MapFrom(c => Enumeration.NameFromValue<CategoryType>(
                        c.Options.CategoryType.Value)));
    }
}