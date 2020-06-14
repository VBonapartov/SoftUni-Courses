namespace BookShop.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using BookShop.Models.Categories;
    using BookShop.Services.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;      

    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        private readonly IMapper mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await this.categoryService.All();

            if (categories == null)
            {
                return NotFound();
            }

            var categoriesModel = mapper.Map<IEnumerable<CategoryModel>>(categories);

            return View(categoriesModel);
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

            var categoryNameExists = await this.categoryService.Exists(model.Name);
            if (categoryNameExists)
            {
                ModelState.AddModelError(nameof(CategoryModel.Name), "Category name already exists.");
                return View(model);
            }

            var id = await this.categoryService.Create(model.Name);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await this.categoryService.Details(id);

            if (category == null)
            {                
                return RedirectToAction(nameof(All));
            }

            var categoryModel = mapper.Map<CategoryModel>(category);

            return View(categoryModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), id);
            }

            var categoryExists = await this.categoryService.Exists(id);
            if (!categoryExists)
            {
                return NotFound("Category does not exist.");
            }

            model.Name = model.Name.Trim();

            var categoryNameExists = await this.categoryService.Exists(id, model.Name);
            if (categoryNameExists)
            {
                ModelState.AddModelError(nameof(CategoryModel.Name), "Category name already exists.");
                return View(model);
            }

            await this.categoryService.Update(id, model.Name);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await this.categoryService.Details(id);

            if (category == null)
            {                
                return RedirectToAction(nameof(All));
            }

            var categoryModel = mapper.Map<CategoryModel>(category);

            return View(categoryModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var categoryExists = await this.categoryService.Exists(id);
            if (!categoryExists)
            {
                return RedirectToAction(nameof(All));
            }

            await this.categoryService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}