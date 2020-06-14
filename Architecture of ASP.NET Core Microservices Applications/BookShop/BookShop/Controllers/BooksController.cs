namespace BookShop.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using BookShop.Models.Books;
    using BookShop.Services.Authors;
    using BookShop.Services.Books;
    using BookShop.Services.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class BooksController : Controller
    {
        private readonly IAuthorService authorService;

        private readonly ICategoryService categoryService;

        private readonly IBookService bookService;

        private readonly IMapper mapper;

        public BooksController(IAuthorService authorService,
                               ICategoryService categoryService,
                               IBookService bookService, 
                               IMapper mapper)
        {
            this.authorService = authorService;
            this.categoryService = categoryService;
            this.bookService = bookService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var books = await this.bookService.All();

            if (books == null)
            {
                return NotFound();
            }

            var booksModel = mapper.Map<IEnumerable<BookListingModel>>(books);

            return View(booksModel);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await this.bookService.Details(id);

            if (bookDetails == null)
            {
                return NotFound();
            }

            var bookDetailsModel = mapper.Map<BookDetailsModel>(bookDetails);

            return View(bookDetailsModel);
        }

        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] string search = "")
        //{
        //    var bookListing = await this._db
        //            .Books
        //            .Where(b => b.Title.ToLower().Contains(search.ToLower()))
        //            .OrderBy(b => b.Title)
        //            .Take(10)
        //            .ProjectTo<BookListingModel>(_mapper.ConfigurationProvider)
        //            .ToListAsync();

        //    if (bookListing == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bookListing);
        //}           

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add()
        {
            var authors = await this.authorService.List();
            var authorList = authors
                .ToList()
                .Select(a => new SelectListItem 
                {
                    Value = $"{a.Id}",
                    Text = $"{a.Author}"
                })
                .ToList();

            var categories = await this.categoryService.All();
            var categoryList = categories
                .ToList()
                .Select(c => new SelectListItem
                {
                    Value = $"{c.Id}",
                    Text = $"{c.Name}"
                })
                .ToList();

            var booksModel = new BookDetailsModel
            { 
                Authors = authorList,
                CategoryList = categoryList,
                ReleaseDate = DateTime.Now
            };

            return View(booksModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(BookDetailsModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Add));
            }

            var authorExists = await this.authorService.Exists(model.AuthorId);
            if (!authorExists)
            {
                ModelState.AddModelError(nameof(BookDetailsModel.AuthorId), "Author does not exist.");
                return View(model);
            }

            var id = await this.bookService.Create(
                model.Title.Trim(),
                model.Description.Trim(),
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                model.AuthorId,
                model.CategoriesId);

          return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await this.bookService.Details(id);

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            var bookModel = mapper.Map<BookDetailsModel>(book);

            var authors = await this.authorService.List();
            var authorList = authors
                .ToList()
                .Select(a => new SelectListItem
                {
                    Value = $"{a.Id}",
                    Text = $"{a.Author}"
                })
                .ToList();

            var categories = await this.categoryService.All();
            var categoryList = categories
                .ToList()
                .Select(c => new SelectListItem
                {
                    Value = $"{c.Id}",
                    Text = $"{c.Name}"
                })
                .ToList();

            bookModel.Authors = authorList;
            bookModel.CategoryList = categoryList;

            return View(bookModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, BookDetailsModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), id);
            }

            var bookExists = await this.bookService.Exists(id);
            if (!bookExists)
            {
                ModelState.AddModelError(nameof(BookDetailsModel.Title), "Book does not exist.");
                return View(model);
            }

            var authorExists = await this.authorService.Exists(model.AuthorId);
            if (!authorExists)
            {
                ModelState.AddModelError(nameof(BookDetailsModel.AuthorId), "Author does not exist.");
                return View(model);
            }

            await this.bookService.Update(
                id,
                model.Title.Trim(),
                model.Description.Trim(),
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                model.AuthorId,
                model.CategoriesId);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await this.bookService.LessDetails(id);

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            var bookModel = mapper.Map<BookListingModel>(book);

            return View(bookModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var bookExists = await this.bookService.Exists(id);
            if (!bookExists)
            {
                return RedirectToAction(nameof(All));
            }

            await this.bookService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}