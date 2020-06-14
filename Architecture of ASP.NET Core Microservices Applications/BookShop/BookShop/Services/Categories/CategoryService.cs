namespace BookShop.Services.Categories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Services.Models.Categories;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly BookShopDbContext db;

        private readonly IMapper mapper;

        public CategoryService(BookShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CategoryServiceModel>> All()
            => await this.db
                .Categories
                .AsNoTracking()
                .ProjectTo<CategoryServiceModel>(mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<CategoryServiceModel> Details(int id)
            => await this.db
                .Categories
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ProjectTo<CategoryServiceModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<bool> Exists(int id)
            => await this.db
                .Categories
                .AnyAsync(c => c.Id == id);

        public async Task<bool> Exists(string name)
            => await this.db
                .Categories                
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());

        public async Task<bool> Exists(int id, string name)
            => await this.db
                .Categories
                .Where(c => c.Id != id)
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());

        public async Task<int> Create(string name)
        {
            var category = new Category { Name = name };

            await this.db.Categories.AddAsync(category);
            await this.db.SaveChangesAsync();

            return category.Id;
        }

        public async Task Update(int id, string name)
        {
            var category = await this.db.Categories.FindAsync(id);
            if (category == null)
            {
                return;
            }

            if (category.Name != name)
            {
                category.Name = name;

                await this.db.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var category = await this.db.Categories.FindAsync(id);
            if (category == null)
            {
                return;
            }

            this.db.Remove(category);
            await this.db.SaveChangesAsync();
        }
    }
}