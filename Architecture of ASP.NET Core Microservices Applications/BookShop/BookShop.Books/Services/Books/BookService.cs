namespace BookShop.Books.Services.Books
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookShop.Books.Data;
    using BookShop.Books.Data.Models;
    using BookShop.Books.Services.Models.Books;
    using BookShop.Messages.Books;
    using BookShop.Services.Identity;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;

    public class BookService : IBookService
    {
        private readonly BooksDbContext db;

        private readonly ICurrentUserService user;

        private readonly IMapper mapper;

        private readonly IBus publisher;

        public BookService(BooksDbContext db,
            ICurrentUserService user,
            IMapper mapper, 
            IBus publisher)
        {
            this.db = db;
            this.user = user;
            this.mapper = mapper;
            this.publisher = publisher;
        }

        public async Task<IEnumerable<BookListingServiceModel>> All()
            => await this.db
                .Books
                .AsNoTracking() 
                .OrderBy(b => b.Title)         
                .ProjectTo<BookListingServiceModel>(mapper.ConfigurationProvider)                
                .ToListAsync();

        public async Task<BookDetailsServiceModel> Details(int id)
        {
            var details = await this.db
                  .Books
                  .AsNoTracking()
                  .Where(b => b.Id == id)
                  .ProjectTo<BookDetailsServiceModel>(mapper.ConfigurationProvider)
                  .FirstOrDefaultAsync();

            await this.publisher.Publish(new BookViewedMessage
            {
                BookId = id,
                UserId = string.IsNullOrEmpty(this.user.UserId)
                        ? string.Empty
                        : this.user.UserId
            });

            return details;
        }

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

            await this.publisher.Publish(new BookCreatedMessage
            {
                BookId = book.Id,
                Title = book.Title,
                Price = book.Price
            });

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