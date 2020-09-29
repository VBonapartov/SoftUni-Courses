namespace BookShop.Infrastructure.Books.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Books.Books;
    using Application.Books.Books.Queries.Common;
    using Application.Books.Books.Queries.Details;
    using Application.Books.Books.Queries.Publishers;
    using AutoMapper;    
    using Common;
    using Domain.Books.Models.Authors;
    using Domain.Books.Models.Books;
    using Domain.Books.Repositories;
    using Domain.Common;
    using Infrastructure.Common.Persistence;
    using Microsoft.EntityFrameworkCore;    

    internal class BookRepository : DataRepository<IBooksDbContext, Book>, IBookQueryRepository, IBookDomainRepository
    {
        private readonly IMapper mapper;

        public BookRepository(BookShopDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public async Task<Book> Find(int id, CancellationToken cancellationToken = default)
            => await this
                .All()
                //.Include(c => c.Publisher)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var book = await this.Data.Books.FindAsync(id);

            if (book == null)
            {
                return false;
            }

            this.Data.Books.Remove(book);

            await this.Data.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<Publisher> GetPublisher(
            string publisher,
            CancellationToken cancellationToken = default)
            => await this
                .Data
                .Publishers
                .FirstOrDefaultAsync(p => p.Name == publisher, cancellationToken);

        public async Task<IEnumerable<TOutputModel>> GetBookListings<TOutputModel>(
            Specification<Book> bookSpecification,
            Specification<Author> authorSpecification,
            BooksSortOrder booksSortOrder,
            int skip = 0,
            int take = int.MaxValue,
            CancellationToken cancellationToken = default)
            => (await this.mapper
                .ProjectTo<TOutputModel>(this
                    .GetBooksQuery(bookSpecification, authorSpecification)
                    .Sort(booksSortOrder))
                .ToListAsync(cancellationToken))
                .Skip(skip)
                .Take(take); // EF Core bug forces me to execute paging on the client.

        public async Task<BookDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<BookDetailsOutputModel>(this
                    .AllAvailable()
                    .Where(c => c.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<IEnumerable<GetBookPublisherOutputModel>> GetBookPublishers(
            CancellationToken cancellationToken = default)
        {
            var publishers = await this.mapper
                .ProjectTo<GetBookPublisherOutputModel>(this.Data.Publishers)
                .ToDictionaryAsync(k => k.Id, cancellationToken);

            var booksPerPublisher = await this.AllAvailable()
                .GroupBy(b => b.Publisher.Id)
                .Select(gr => new
                {
                    PublisherId = gr.Key,
                    TotalBooks = gr.Count()
                })
                .ToListAsync(cancellationToken);

            booksPerPublisher.ForEach(b => publishers[b.PublisherId].TotalBooks = b.TotalBooks);

            return publishers.Values;
        }

        public async Task<int> Total(
            Specification<Book> bookSpecification,
            Specification<Author> authorSpecification,
            CancellationToken cancellationToken = default)
            => await this
                .GetBooksQuery(bookSpecification, authorSpecification)
                .CountAsync(cancellationToken);

        private IQueryable<Book> AllAvailable()
            => this
                .All()
                .Where(book => book.IsAvailable);

        private IQueryable<Book> GetBooksQuery(
            Specification<Book> bookSpecification,
            Specification<Author> authorSpecification)
            => this
                .Data
                .Authors
                .Where(authorSpecification)
                .SelectMany(a => a.Books)
                .Where(bookSpecification);
    }
}