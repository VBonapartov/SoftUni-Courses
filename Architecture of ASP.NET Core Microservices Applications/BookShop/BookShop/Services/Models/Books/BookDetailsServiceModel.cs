namespace BookShop.Services.Models.Books
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using BookShop.Data.Models;
    using BookShop.Infrastructure.Mapping;

    public class BookDetailsServiceModel : IMapFrom<Book>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public int? Edition { get; set; }

        public int? AgeRestriction { get; set; }

        public DateTime ReleaseDate { get; set; }

        public IEnumerable<int> CategoriesId { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Book, BookDetailsServiceModel>()
                .ForMember(b => b.CategoriesId, cfg => cfg
                    .MapFrom(b => b.Categories.Select(c => c.Category.Id)))
                .ForMember(b => b.Categories, cfg => cfg
                    .MapFrom(b => b.Categories.Select(c => c.Category.Name)))
                .ForMember(b => b.Author, cfg => cfg
                    .MapFrom(b => $"{b.Author.FirstName} {b.Author.LastName}"));
        }
    }
}