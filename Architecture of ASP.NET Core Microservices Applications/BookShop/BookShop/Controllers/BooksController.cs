namespace BookShop.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Models.Books;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class BooksController : Controller
    {
        private readonly BookShopDbContext _db;

        private readonly IMapper _mapper;

        public BooksController(BookShopDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var books = await this._db
                .Books
                .ProjectTo<BookListingModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await this._db
                     .Books
                     .Where(b => b.Id == id)
                     .ProjectTo<BookDetailsModel>(_mapper.ConfigurationProvider)
                     .FirstOrDefaultAsync();

            if (bookDetails == null)
            {
                return NotFound();
            }

            return View(bookDetails);
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
            var authors = await this._db.Authors
                .Select(a => new SelectListItem {
                    Value = $"{a.Id}",
                    Text = $"{a.FirstName} {a.LastName}"
                })
                .ToListAsync();

            var categories = await this._db.Categories
                .Select(c => new SelectListItem
                {
                    Value = $"{c.Id}",
                    Text = $"{c.Name}"
                })
                .ToListAsync();

            var booksModel = new BookDetailsModel
            { 
                Authors = authors,
                CategoryList = categories,
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

            var authorExists = await this._db.Authors.AnyAsync(a => a.Id == model.AuthorId);
            if (!authorExists)
            {
                ModelState.AddModelError(nameof(BookDetailsModel.AuthorId), "Author does not exist.");
                return View(model);
            }

            // Create book
            var book = new Book
            {
                AuthorId = model.AuthorId,
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                Copies = model.Copies,
                Edition = model.Edition,
                ReleaseDate = model.ReleaseDate,
                AgeRestriction = model.AgeRestriction      
            };

            // Get Selected Categories
            var selectedCategories = await this._db
                .Categories
                .Where(c => model.CategoriesId.Contains(c.Id))
                .ToListAsync();

            // Add Categories to Book
            foreach (var category in selectedCategories)
            {
                book.Categories.Add(new CategoryBook { CategoryId = category.Id });
            }

            await this._db.AddAsync(book);
            await this._db.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await this._db
                             .Books
                             .Where(b => b.Id == id)
                             .ProjectTo<BookDetailsModel>(_mapper.ConfigurationProvider)
                             .FirstOrDefaultAsync();

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            var authors = await this._db.Authors
                            .Select(a => new SelectListItem
                            {
                                Value = $"{a.Id}",
                                Text = $"{a.FirstName} {a.LastName}"
                            })
                            .ToListAsync();

            var categories = await this._db.Categories
                            .Select(c => new SelectListItem
                            {
                                Value = $"{c.Id}",
                                Text = $"{c.Name}"
                            })
                            .ToListAsync();

            book.Authors = authors;
            book.CategoryList = categories;

            return this.View(book);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, BookDetailsModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), id);
            }

            var book = await this._db.Books.FindAsync(id);
            if (book == null)
            {
                ModelState.AddModelError(nameof(BookDetailsModel.Title), "Book does not exist.");
                return View(model);
            }

            var authorExists = await this._db.Authors.AnyAsync(a => a.Id == model.AuthorId);
            if (!authorExists)
            {
                ModelState.AddModelError(nameof(BookDetailsModel.AuthorId), "Author does not exist.");
                return View(model);
            }   

            book.AuthorId = model.AuthorId;
            book.Title = model.Title;
            book.Description = model.Description;
            book.Price = model.Price;
            book.Copies = model.Copies;
            book.Edition = model.Edition;
            book.ReleaseDate = model.ReleaseDate;
            book.AgeRestriction = model.AgeRestriction;

            // Get Selected Categories
            var selectedCategories = await this._db
                .Categories
                .Where(c => model.CategoriesId.Contains(c.Id))
                .ToListAsync();

            // Clear categories
            var categoryBooks = await this._db
                .CategoryBooks
                .Where(cb => cb.BookId == book.Id)
                .ToListAsync();

            foreach (var categoryBook in categoryBooks)
            {
                book.Categories.Remove(categoryBook);
            }

            // Add Categories to Book
            foreach (var category in selectedCategories)
            {
                book.Categories.Add(new CategoryBook { CategoryId = category.Id });
            }

            await this._db.SaveChangesAsync();           

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await this._db
                .Books
                .Where(c => c.Id == id)
                .ProjectTo<BookListingModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(book);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var book = await this._db.Books.FindAsync(id);
            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            this._db.Remove(book);
            await this._db.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
    }
}