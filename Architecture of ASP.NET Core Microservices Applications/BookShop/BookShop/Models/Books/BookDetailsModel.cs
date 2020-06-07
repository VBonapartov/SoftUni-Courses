namespace BookShop.Models.Books
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using BookShop.Data.Models;
    using BookShop.Infrastructure.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class BookDetailsModel : BookWithCategoriesModel, IMapFrom<Book>, IHaveCustomMapping
    {
        [Required]
        [DisplayName("Author")]
        public int AuthorId { get; set; }

        public string Author { get; set; }

        public List<SelectListItem> Authors { get; set; }

        public List<SelectListItem> CategoryList { get; set; }

        public override void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Book, BookDetailsModel>()
                .ForMember(b => b.CategoriesId, cfg => cfg
                    .MapFrom(b => b.Categories.Select(c => c.Category.Id)))
                .ForMember(b => b.Categories, cfg => cfg
                    .MapFrom(b => b.Categories.Select(c => c.Category.Name)))
                .ForMember(b => b.Author, cfg => cfg
                    .MapFrom(b => $"{b.Author.FirstName} {b.Author.LastName}"));
        }
    }
}