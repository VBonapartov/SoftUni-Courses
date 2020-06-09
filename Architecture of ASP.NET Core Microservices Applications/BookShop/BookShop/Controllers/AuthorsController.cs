namespace BookShop.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Models.Authors;
    using BookShop.Models.Books;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class AuthorsController : Controller
    {
        private readonly BookShopDbContext _db;

        private readonly IMapper _mapper;

        public AuthorsController(BookShopDbContext db,  IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var authors = await this._db
                .Authors
                .ProjectTo<AuthorDetailsModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();

            if (authors == null)
            {
                return NotFound();
            }

            return View(authors);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var authorDetails = await this._db
                        .Authors
                        .Where(a => a.Id == id)
                        .ProjectTo<AuthorDetailsModel>(_mapper.ConfigurationProvider)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

            if (authorDetails == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(authorDetails);
        }

        [HttpGet]
        [Route("{id}/books")]
        public async Task<IActionResult> GetBooks(int id)
        {
            var authorBooks = await this._db
                            .Books
                            .Where(b => b.AuthorId == id)
                            .ProjectTo<BookWithCategoriesModel>(_mapper.ConfigurationProvider)
                            .AsNoTracking()
                            .ToListAsync();

            if (authorBooks == null)
            {
                return NotFound();
            }

            return View(authorBooks);
        }

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

            var author = new Author
            {
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim()
            };

            await this._db.Authors.AddAsync(author);
            await this._db.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
    }
}