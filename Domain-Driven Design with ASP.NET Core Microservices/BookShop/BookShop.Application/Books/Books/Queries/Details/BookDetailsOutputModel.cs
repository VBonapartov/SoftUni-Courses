namespace BookShop.Application.Books.Books.Queries.Details
{
    using AutoMapper;
    using Application.Books.Authors.Queries.Common;
    using Common;
    using Domain.Books.Models.Books;
    using Domain.Common.Models;

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