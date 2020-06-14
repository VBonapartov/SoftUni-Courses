namespace BookShop.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using BookShop.Models.Authors;
    using BookShop.Services.Authors;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AuthorsController : Controller
    {
        private readonly IAuthorService authorService;

        private readonly IMapper mapper;

        public AuthorsController(IAuthorService authorService,  IMapper mapper)
        {
            this.authorService = authorService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var authors = await this.authorService.All();

            if (authors == null)
            {
                return NotFound();
            }

            var authorsModel = mapper.Map<IEnumerable<AuthorDetailsModel>>(authors);

            return View(authorsModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var author = await this.authorService.Details(id);

            if (author == null)
            {
                return RedirectToAction(nameof(All));
            }

            var authorModel = mapper.Map<AuthorDetailsModel>(author);

            return View(authorModel);
        }

        //[HttpGet]
        //[Route("{id}/books")]
        //public async Task<IActionResult> GetBooks(int id)
        //{
        //    var authorBooks = await this._db
        //                    .Books
        //                    .Where(b => b.AuthorId == id)
        //                    .ProjectTo<BookWithCategoriesModel>(_mapper.ConfigurationProvider)
        //                    .AsNoTracking()
        //                    .ToListAsync();

        //    if (authorBooks == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(authorBooks);
        //}

        [HttpGet]
        [Authorize]
        public IActionResult Add() => View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AuthorDetailsModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Add));
            }

            await this.authorService.Create(model.FirstName, model.LastName);

            return RedirectToAction(nameof(All));
        }
    }
}