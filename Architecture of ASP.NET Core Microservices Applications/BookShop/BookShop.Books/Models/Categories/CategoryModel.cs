namespace BookShop.Books.Models.Categories
{
    using System.ComponentModel.DataAnnotations;
    using BookShop.Books.Services.Models.Categories;
    using BookShop.Models;

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