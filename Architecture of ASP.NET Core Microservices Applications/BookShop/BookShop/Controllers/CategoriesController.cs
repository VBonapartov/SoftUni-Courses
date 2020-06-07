namespace BookShop.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Models.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;   
    using Microsoft.EntityFrameworkCore;    

    public class CategoriesController : Controller
    {
        private readonly BookShopDbContext _db;

        private readonly IMapper _mapper;

        public CategoriesController(BookShopDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await this._db
                .Categories
                .ProjectTo<CategoryModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }            

        //[HttpGet]
        //public async Task<IActionResult> Get(int id)
        //{
        //     var category = await this._db
        //                .Categories
        //                .Where(c => c.Id == id)
        //                .ProjectTo<CategoryModel>(_mapper.ConfigurationProvider)
        //                .FirstOrDefaultAsync();

        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}       

        [HttpGet]
        [Authorize]
        public IActionResult Add() => View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Add));
            }

            model.Name = model.Name.Trim();

            var categoryNameExists = await this._db
                        .Categories
                        .AnyAsync(c => c.Name.ToLower() == model.Name.ToLower());
            
            if (categoryNameExists)
            {
                ModelState.AddModelError(nameof(CategoryModel.Name), "Category name already exists.");
                return View(model);
            }

            var category = new Category { Name = model.Name };

            await this._db.Categories.AddAsync(category);
            await this._db.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await this._db
                             .Categories
                             .Where(c => c.Id == id)
                             .ProjectTo<CategoryModel>(_mapper.ConfigurationProvider)
                             .FirstOrDefaultAsync();

            if (category == null)
            {                
                return RedirectToAction(nameof(All));
            }

            return View(category);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), id);
            }

            var category = await this._db.Categories.FindAsync(id);
            if (category == null)
            {
                ModelState.AddModelError(nameof(CategoryModel.Name), "Category does not exist.");
                return View(model);
            }

            model.Name = model.Name.Trim();

            var categoryNameExists = await this._db
                        .Categories
                        .Where(c => c.Id != id)
                        .AnyAsync(c => c.Name.ToLower() == model.Name.ToLower());
            
            if (categoryNameExists)
            {
                ModelState.AddModelError(nameof(CategoryModel.Name), "Category name already exists.");
                return View(model);
            }

            if (category.Name != model.Name)
            {
                category.Name = model.Name;
                await this._db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await this._db
                .Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (category == null)
            {                
                return RedirectToAction(nameof(All));
            }

            return View(category);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var category = await this._db.Categories.FindAsync(id);
            if (category == null)
            {
                return RedirectToAction(nameof(All));
            }

            this._db.Remove(category);
            await this._db.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
    }
}
