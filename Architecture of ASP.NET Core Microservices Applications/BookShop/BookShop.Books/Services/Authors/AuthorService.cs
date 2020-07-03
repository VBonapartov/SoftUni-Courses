namespace BookShop.Books.Services.Authors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookShop.Books.Data;
    using BookShop.Books.Data.Models;
    using BookShop.Books.Services.Models.Authors;
    using Microsoft.EntityFrameworkCore;

    public class AuthorService : IAuthorService
    {
        private readonly BooksDbContext db;

        private readonly IMapper mapper;

        public AuthorService(BooksDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDetailsServiceModel>> All()
            => await this.db
                      .Authors
                      .AsNoTracking()
                      .ProjectTo<AuthorDetailsServiceModel>(mapper.ConfigurationProvider)
                      .ToListAsync();

        public async Task<AuthorDetailsServiceModel> Details(int id)
            => await this.db
                        .Authors
                        .AsNoTracking()
                        .Where(a => a.Id == id)
                        .ProjectTo<AuthorDetailsServiceModel>(mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();

        public async Task<IEnumerable<AuthorListingServiceModel>> List()
            => await this.db
                      .Authors
                      .AsNoTracking()
                      .ProjectTo<AuthorListingServiceModel>(mapper.ConfigurationProvider)
                      .ToListAsync();

        public async Task<int> Create(string firstName, string lastName)
        {
            var author = new Author
            {
                FirstName = firstName,
                LastName = lastName
            };

            await this.db.Authors.AddAsync(author);
            await this.db.SaveChangesAsync();

            return author.Id;
        }

        public async Task<bool> Exists(int id)
            => await this.db.Authors.AnyAsync(a => a.Id == id);     
    }
}