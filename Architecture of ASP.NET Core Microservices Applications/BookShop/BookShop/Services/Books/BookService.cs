namespace BookShop.Services.Books
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Services.Models.Books;
    using Microsoft.EntityFrameworkCore;

    public class BookService : IBookService
    {
        private readonly BookShopDbContext db;

        private readonly IMapper mapper;

        public BookService(BookShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookListingServiceModel>> All()
            => await this.db
                .Books
                .AsNoTracking() 
                .OrderBy(b => b.Title)         
                .ProjectTo<BookListingServiceModel>(mapper.ConfigurationProvider)                
                .ToListAsync();

        public async Task<BookDetailsServiceModel> Details(int id)
            => await this.db
                .Books
                .AsNoTracking()
                .Where(b => b.Id == id)
                .ProjectTo<BookDetailsServiceModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<BookListingServiceModel> LessDetails(int id)
            => await this.db
                .Books
                .AsNoTracking()
                .Where(b => b.Id == id)
                .ProjectTo<BookListingServiceModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<bool> Exists(int id)
            => await this.db.Books.AnyAsync(b => b.Id == id);

        public async Task<int> Create(
                                 string title,
                                 string description,
                                 decimal price,
                                 int copies,
                                 int? edition,
                                 int? ageRestriction,
                                 DateTime? releaseDate,
                                 int authorId,
                                 IEnumerable<int> categoriesId)
        {
            // Create book
            var book = new Book
            {
                AuthorId = authorId,
                Title = title,
                Description = description,
                Price = price,
                Copies = copies,
                Edition = edition,
                ReleaseDate = releaseDate,
                AgeRestriction = ageRestriction
            };

            // Get Selected Categories
            var selectedCategories = await this.db
                .Categories
                .Where(c => categoriesId.Contains(c.Id))
                .AsNoTracking()
                .ToListAsync();

            // Add Categories to Book
            foreach (var category in selectedCategories)
            {
                book.Categories.Add(new CategoryBook { CategoryId = category.Id });
            }

            await this.db.AddAsync(book);
            await this.db.SaveChangesAsync();

            return book.Id;
        }

        public async Task Update(
            int id,
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime? releaseDate,
            int authorId,
            IEnumerable<int> categoriesId)
        {
            var book = await this.db.Books.FindAsync(id);
            if (book == null)
            {
                return;
            }
                        
            book.AuthorId = authorId;
            book.Title = title;
            book.Description = description;
            book.Price = price;
            book.Copies = copies;
            book.Edition = edition;
            book.ReleaseDate = releaseDate;
            book.AgeRestriction = ageRestriction;

            // Get Selected Categories
            var selectedCategories = await this.db
                .Categories
                .AsNoTracking()
                .Where(c => categoriesId.Contains(c.Id))
                .ToListAsync();

            // Clear categories
            var categoryBooks = await this.db
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

            await this.db.SaveChangesAsync();            
        }

        public async Task Delete(int id)
        {
            var book = await this.db.Books.FindAsync(id);
            if (book == null)
            {
                return;
            }

            this.db.Remove(book);
            await this.db.SaveChangesAsync();
        }
    }
}