namespace BookShop.Infrastructure.Books.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Books.Authors;
    using Application.Books.Authors.Queries.Common;
    using Application.Books.Authors.Queries.Details;
    using AutoMapper;    
    using Domain.Books.Exceptions;
    using Domain.Books.Models.Authors;
    using Domain.Books.Repositories;
    using Infrastructure.Common.Persistence;
    using Microsoft.EntityFrameworkCore;

    internal class AuthorRepository : DataRepository<IBooksDbContext, Author>, IAuthorQueryRepository, IAuthorDomainRepository
    {
        private readonly IMapper mapper;

        public AuthorRepository(IBooksDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public Task<Author> FindByUser(
            string userId,
            CancellationToken cancellationToken = default)
            => this.FindByUser(userId, author => author, cancellationToken);

        public Task<int> GetAuthorId(
            string userId,
            CancellationToken cancellationToken = default)
            => this.FindByUser(userId, author => author.Id, cancellationToken);

        public async Task<bool> HasBook(int authorId, int bookId, CancellationToken cancellationToken = default)
            => await this
                .All()
                .Where(a => a.Id == authorId)
                .AnyAsync(a => a.Books
                    .Any(b => b.Id == bookId), cancellationToken);

        public async Task<AuthorDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<AuthorDetailsOutputModel>(this
                    .All()
                    .Where(d => d.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<AuthorOutputModel> GetDetailsByBookId(int bookId, CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<AuthorOutputModel>(this
                    .All()
                    .Where(a => a.Books.Any(c => c.Id == bookId)))
                .SingleOrDefaultAsync(cancellationToken);

        private async Task<T> FindByUser<T>(
            string userId,
            Expression<Func<Author, T>> selector,
            CancellationToken cancellationToken = default)
        {
            var authorData = await this
                .All()
                .Where(a => a.UserId == userId)
                .Select(selector)
                .FirstOrDefaultAsync(cancellationToken);

            if (authorData == null)
            {
                throw new InvalidAuthorException("This user is not a author.");
            }

            return authorData;
        }
    }
}