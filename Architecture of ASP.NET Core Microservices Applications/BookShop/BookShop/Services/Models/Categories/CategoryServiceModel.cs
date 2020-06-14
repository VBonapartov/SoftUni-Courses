namespace BookShop.Services.Models.Categories
{
    using BookShop.Data.Models;
    using BookShop.Infrastructure.Mapping;

    public class CategoryServiceModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}