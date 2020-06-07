namespace BookShop.Models.Categories
{
    using System.ComponentModel.DataAnnotations;
    using BookShop.Data.Models;
    using BookShop.Infrastructure.Mapping;        

    public class CategoryModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}