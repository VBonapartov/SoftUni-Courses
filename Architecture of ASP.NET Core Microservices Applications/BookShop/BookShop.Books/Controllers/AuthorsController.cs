namespace BookShop.Books.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using BookShop.Books.Models.Authors;
    using BookShop.Books.Services.Authors;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AuthorsController : AdministrationController
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
        public IActionResult Add() => View();

        [HttpPost]
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