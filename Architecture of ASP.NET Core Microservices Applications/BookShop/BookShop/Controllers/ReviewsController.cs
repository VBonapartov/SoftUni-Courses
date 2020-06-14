namespace BookShop.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using BookShop.Models.Reviews;
    using BookShop.Services.Books;
    using BookShop.Services.Reviews;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ReviewsController : Controller
    {
        private readonly IBookService bookService;

        private readonly IReviewService reviewService;

        private readonly IMapper mapper;

        public ReviewsController(IBookService bookService, IReviewService reviewService,  IMapper mapper)
        {
            this.bookService = bookService;
            this.reviewService = reviewService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var reviews = await this.reviewService.All();

            if (reviews == null)
            {
                return NotFound();
            }

            var reviewsModel = mapper.Map<List<ReviewListingModel>>(reviews);
            reviewsModel.ToList().ForEach(r =>
            {
                r.Description = r.Description.Substring(0, Math.Min(r.Description.Length, 300)) + ".....";
            }); 

            return View(reviewsModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var review = await this.reviewService.Details(id);

            if (review == null)
            {
                return NotFound();
            }

            var reviewModel = mapper.Map<ReviewListingModel>(review);

            return View(reviewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add()
        {
            var books = await this.bookService.All();
            var bookList = books
                    .Select(b => new SelectListItem
                    {
                        Value = $"{b.Id}",
                        Text = $"{b.Title}"
                    })
                    .ToList();

            var review = new ReviewListingModel
            {
                Books = bookList
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

            var bookExists = await this.bookService.Exists(model.BookId);
            if (!bookExists)
            {
                ModelState.AddModelError(nameof(ReviewListingModel.BookId), "Book does not exist.");
                return View(model);
            }

            await this.reviewService
                .Create(model.Title, 
                        model.Description, 
                        model.Author, 
                        model.BookId);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var review = await this.reviewService.Details(id);

            if (review == null)
            {
                return RedirectToAction(nameof(All));
            }

            var reviewModel = mapper.Map<ReviewListingModel>(review);

            var books = await this.bookService.All();
            var bookList = books
                    .Select(b => new SelectListItem
                    {
                        Value = $"{b.Id}",
                        Text = $"{b.Title}"
                    })
                    .ToList();

            reviewModel.Books = bookList;

            return View(reviewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, ReviewListingModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), id);
            }

            var reviewExists = await this.reviewService.Exists(id);
            if (!reviewExists)
            {
                ModelState.AddModelError(nameof(ReviewListingModel.Title), "Review does not exist.");
                return View(model);
            }

            var bookExists = await this.bookService.Exists(model.BookId);
            if (!bookExists)
            {
                ModelState.AddModelError(nameof(ReviewListingModel.BookId), "Book does not exist.");
                return View(model);
            }

            await this.reviewService.Update(
                id,
                model.Title.Trim(),
                model.Description.Trim(),
                model.Author.Trim(),
                model.BookId);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await this.reviewService.Details(id);

            if (review == null)
            {
                return RedirectToAction(nameof(All));
            }

            var reviewModel = mapper.Map<ReviewListingModel>(review);

            return View(reviewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var reviewExists = await this.reviewService.Exists(id);
            if (!reviewExists)
            {
                return RedirectToAction(nameof(All));
            }          

            await this.bookService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}