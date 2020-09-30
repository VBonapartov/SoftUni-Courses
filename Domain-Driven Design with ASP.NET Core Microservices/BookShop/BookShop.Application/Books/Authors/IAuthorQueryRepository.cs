namespace BookShop.Application.Books.Authors
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Contracts;
    using Domain.Books.Models.Authors;
    using Queries.Common;
    using Queries.Details;    

    public interface IAuthorQueryRepository : IQueryRepository<Author>
    {
       Task<AuthorDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default);

        Task<AuthorOutputModel> GetDetailsByBookId(int bookId, CancellationToken cancellationToken = default);
    }
}