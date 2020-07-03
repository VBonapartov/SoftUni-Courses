namespace BookShop.Books.Services.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookShop.Books.Services.Models.Categories;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryServiceModel>> All();

        Task<CategoryServiceModel> Details(int id);

        Task<bool> Exists(int id);

        Task<bool> Exists(string name);

        Task<bool> Exists(int id, string name);

        Task<int> Create(string name);

        Task Update(int id, string name);

        Task Delete(int id);
    }
}