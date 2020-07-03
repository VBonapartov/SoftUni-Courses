namespace BookShop.Books.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using BookShop.Books.Models.Reviews;
    using BookShop.Books.Services.Books;
    using BookShop.Books.Services.Models.Reviews;
    using BookShop.Books.Services.Reviews;
    using BookShop.Infrastructure;
    using BookShop.Services.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ReviewsController : BaseController
    {
        private readonly ICurrentUserService userService;

        private readonly IBookService bookService;

        private readonly IReviewService reviewService;

        private readonly IMapper mapper;

        public ReviewsController(ICurrentUserService userService, 
            IBookService bookService, 
            IReviewService reviewService,
            IMapper mapper)
        {
            this.userService = userService;
            this.bookService = bookService;
            this.reviewService = reviewService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            List<ReviewListingModel> reviewsModel = new List<ReviewListingModel>();

            bool result = await this.Handle(
                async () =>
                {
                    var reviews = await this.reviewService.All();

                    reviewsModel = mapper.Map<List<ReviewListingModel>>(reviews);
                    reviewsModel.ToList().ForEach(r =>
                    {
                        r.Description = r.Description.Substring(0, Math.Min(r.Description.Length, 300)) + ".....";
                    });
                });

            //if (result && reviewsModel.Count > 0)
            return View(reviewsModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string returnUrl)
        {
            ReviewListingModel reviewModel = null;

            var result = await this.Handle(
                async () =>
                {
                    var review = await this.reviewService.Details(id);
                    reviewModel = mapper.Map<ReviewListingModel>(review);
                });

            if (result && reviewModel != null)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(reviewModel);
            }

            return RedirectToAction(nameof(ReviewsController.All));
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

            var book = await this.bookService.Details(model.BookId);

            var review = new ReviewListingServiceModel
            {
                Title = model.Title,
                Description = model.Description,
                AuthorId = userService.UserId,
                Author = userService.Email,
                BookId = model.BookId,
                BookName = book.Title
            };

            return await this.Handle(
                async () =>
                {
                    await this.reviewService.Add(review);
                },
                success: RedirectToAction(nameof(ReviewsController.All)),
                failure: View("../Home/Index", model));
        }

        [HttpGet]
        [AuthorizeAdministrator]
        public async Task<IActionResult> Edit(int id)
        {
            ReviewListingModel reviewModel = null;

            var result = await this.Handle(
                async () =>
                {
                    var review = await this.reviewService.Details(id);
                    reviewModel = mapper.Map<ReviewListingModel>(review);                    
                });

            if (result && reviewModel != null)
            {
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

            return RedirectToAction(nameof(ReviewsController.All));
        }

        [HttpPost]
        [AuthorizeAdministrator]
        public async Task<IActionResult> Edit(int id, ReviewListingModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), id);
            }

            var bookExists = await this.bookService.Exists(model.BookId);
            if (!bookExists)
            {
                ModelState.AddModelError(nameof(ReviewListingModel.BookId), "Book does not exist.");
                return View(model);
            }

            var bookModel = await this.bookService.Details(model.BookId);
            var bookName = bookModel.Title;

            var review = new ReviewListingServiceModel
            {
                Title = model.Title.Trim(),
                Description = model.Description.Trim(),
                AuthorId = model.AuthorId,
                Author = model.Author,
                BookId = model.BookId,
                BookName = bookName
            };

            return await this.Handle(
                async () =>
                {
                    await this.reviewService.Edit(id, review);
                },
                success: RedirectToAction(nameof(ReviewsController.All)),
                failure: View("../Home/Index", model));
        }

        [HttpGet]
        [AuthorizeAdministrator]
        public async Task<IActionResult> Delete(int id)
        {
            ReviewListingModel reviewModel = null;

            var result = await this.Handle(
                async () =>
                {
                    var review = await this.reviewService.Details(id);
                    reviewModel = mapper.Map<ReviewListingModel>(review);
                });

            if (result && reviewModel != null)
                return View(reviewModel);

            return RedirectToAction(nameof(ReviewsController.All));
        }

        [HttpGet]
        [AuthorizeAdministrator]
        public async Task<IActionResult> ConfirmDelete(int id)
        {  
            return await this.Handle(
                 async () =>
                 {
                     await this.reviewService.Delete(id);
                 },
                 success: RedirectToAction(nameof(ReviewsController.All)),
                 failure: RedirectToAction(nameof(ReviewsController.All)));
        }
    }
}