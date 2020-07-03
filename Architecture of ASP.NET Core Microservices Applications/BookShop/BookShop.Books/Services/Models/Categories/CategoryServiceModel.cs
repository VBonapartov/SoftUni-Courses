namespace BookShop.Books.Services.Models.Categories
{
    using BookShop.Books.Data.Models;
    using BookShop.Models;

    public class CategoryServiceModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}