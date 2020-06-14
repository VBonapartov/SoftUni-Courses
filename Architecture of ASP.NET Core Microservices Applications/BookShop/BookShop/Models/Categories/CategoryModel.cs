namespace BookShop.Models.Categories
{
    using System.ComponentModel.DataAnnotations;
    using BookShop.Infrastructure.Mapping;
    using BookShop.Services.Models.Categories;

    using static Data.DataConstants.Category;

    public class CategoryModel : IMapFrom<CategoryServiceModel>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }
    }
}