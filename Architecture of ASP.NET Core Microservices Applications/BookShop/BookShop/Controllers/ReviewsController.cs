namespace BookShop.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Models.Reviews;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class ReviewsController : Controller
    {
        private readonly BookShopDbContext _db;

        private readonly IMapper _mapper;

        public ReviewsController(BookShopDbContext db,  IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var reviews = await this._db
                    .Reviews
                    .Select(r => new ReviewListingModel { 
                        Id = r.Id,
                        Title = r.Title,
                        Description = r.Description.Substring(0, Math.Min(r.Description.Length, 300)) + ".....",
                        Author = r.Author,
                        BookName = r.Book.Title
                    })      
                    .AsNoTracking()
                    .ToListAsync();

            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var review = await this._db
                    .Reviews
                    .Where(r => r.Id == id)
                    .Select(r => new ReviewListingModel
                    {
                        Id = r.Id,
                        Title = r.Title,
                        Description = r.Description,
                        Author = r.Author,
                        BookName = r.Book.Title
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add()
        {
            var books = await this._db.Books
                    .Select(b => new SelectListItem
                    {
                        Value = $"{b.Id}",
                        Text = $"{b.Title}"
                    })
                    .ToListAsync();

            var review = new ReviewListingModel
            {
                Books = books
            };

            return View(review);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(ReviewListingModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Add));
            }

            var review = new Review
            {
                Title = model.Title,
                Description = model.Description,
                Author = model.Author,
                BookId = model.BookId
            };

            await this._db.Reviews.AddAsync(review);
            await this._db.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var review = await this._db
                             .Reviews
                             .Where(c => c.Id == id)
                             .Select(r => new ReviewListingModel
                             {
                                Id = r.Id,
                                Title = r.Title,
                                Description = r.Description,
                                Author = r.Author,
                                BookId = r.Book.Id,
                                BookName = r.Book.Title
                             })
                             .AsNoTracking()
                             .FirstOrDefaultAsync();

            if (review == null)
            {
                return RedirectToAction(nameof(All));
            }

            var books = await this._db.Books
                    .Select(b => new SelectListItem
                    {
                        Value = $"{b.Id}",
                        Text = $"{b.Title}"
                    })
                    .ToListAsync();

            review.Books = books;

            return View(review);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, ReviewListingModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), id);
            }

            var review = await this._db.Reviews.FindAsync(id);
            if (review == null)
            {
                return RedirectToAction(nameof(All), id);
            }

            review.Title = model.Title;
            review.Description = model.Description;
            review.Author = model.Author;
            review.BookId = model.BookId;

            await this._db.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await this._db
                .Reviews
                .Where(c => c.Id == id)
                .Select(r => new ReviewListingModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    //Description = r.Description,
                    //Author = r.Author,
                    BookName = r.Book.Title
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (review == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(review);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var review = await this._db.Reviews.FindAsync(id);
            if (review == null)
            {
                return RedirectToAction(nameof(All));
            }

            this._db.Remove(review);
            await this._db.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
    }
}