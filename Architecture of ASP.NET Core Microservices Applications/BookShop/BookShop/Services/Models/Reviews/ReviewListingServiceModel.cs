namespace BookShop.Services.Models.Reviews
{
    using System;
    using AutoMapper;
    using BookShop.Data.Models;
    using BookShop.Infrastructure.Mapping;

    public class ReviewListingServiceModel : IMapFrom<Review>, IHaveCustomMapping
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string BookName { get; set; }

        public int BookId { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Review, ReviewListingServiceModel>()
                .ForMember(r => r.BookId, cfg => cfg
                    .MapFrom(r => r.Book.Id))
                .ForMember(r => r.BookName, cfg => cfg
                    .MapFrom(r => r.Book.Title));
        }
    }
}