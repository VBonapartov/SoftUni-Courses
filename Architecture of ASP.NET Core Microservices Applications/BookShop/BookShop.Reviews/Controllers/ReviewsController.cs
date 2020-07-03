namespace BookShop.Reviews.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookShop.Controllers;
    using BookShop.Infrastructure;
    using BookShop.Reviews.Models.Reviews;
    using BookShop.Reviews.Services.Reviews;
    using BookShop.Services.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewsController : ApiController
    {
        private readonly IReviewService reviewService;

        private readonly ICurrentUserService currentUser;

        public ReviewsController(IReviewService reviewService, ICurrentUserService currentUser)
        {
            this.reviewService = reviewService;
            this.currentUser = currentUser;
        }

        [HttpGet]
        [Route(nameof(All))]
        public async Task<IEnumerable<ReviewOutputModel>> All()
            =>  await this.reviewService.All();

        [HttpGet]
        [Route(nameof(Details) + "/" + Id)]
        public async Task<ActionResult<ReviewOutputModel>> Details(int id)
            => await this.reviewService.Details(id);

        [HttpGet]
        [Authorize]
        [Route(nameof(Mine))]
        public async Task<IEnumerable<ReviewOutputModel>> Mine()
            => await this.reviewService.Mine(this.currentUser.UserId);

        [HttpPost]
        [Authorize]
        [Route(nameof(Add))]
        public async Task<IActionResult> Add(AddReviewInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest("Invalid input data.");
            }

            await this.reviewService
                .Create(model.Title, 
                        model.Description, 
                        model.AuthorId, 
                        model.Author,
                        model.BookId,
                        model.BookName);

            return Ok();
        }

        [HttpPut]
        [AuthorizeAdministrator]
        [Route(nameof(Edit) + "/" + Id)]
        public async Task<IActionResult> Edit(int id, EditReviewInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest("Invalid input data.");
            }

            var reviewExists = await this.reviewService.Exists(id);
            if (!reviewExists)
            {
                return this.BadRequest("Review does not exist.");
            }

            //var bookExists = await this.bookService.Exists(model.BookId);
            //if (!bookExists)
            //{
            //    ModelState.AddModelError(nameof(ReviewListingModel.BookId), "Book does not exist.");
            //    return View(model);
            //}

            await this.reviewService.Update(
                id,
                model.Title.Trim(),
                model.Description.Trim(),
                model.AuthorId,
                model.Author,
                model.BookId,
                model.BookName);

            return Ok();
        }

        [HttpDelete]
        [AuthorizeAdministrator]
        [Route(nameof(Delete) + "/" + Id)]
        public async Task<IActionResult> Delete(int id)
        {
            var reviewExists = await this.reviewService.Exists(id);
            if (!reviewExists)
            {
                return this.BadRequest("Review does not exist.");
            }          

            await this.reviewService.Delete(id);

            return Ok();
        }
    }
}