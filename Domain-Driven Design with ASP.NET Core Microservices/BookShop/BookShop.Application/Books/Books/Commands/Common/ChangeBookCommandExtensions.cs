namespace BookShop.Application.Books.Books.Commands.Common
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Common.Contracts;
    using Domain.Books.Repositories;

    internal static class ChangeCarAdCommandExtensions
    {
        public static async Task<Result> AuthorHasBook(
            this ICurrentUser currentUser,
            IAuthorDomainRepository authorRepository,
            int bookId,
            CancellationToken cancellationToken)
        {
            var authorId = await authorRepository.GetAuthorId(
                currentUser.UserId,
                cancellationToken);

            var authorHasBook = await authorRepository.HasBook(
                authorId,
                bookId,
                cancellationToken);

            return authorHasBook
                ? Result.Success
                : "You cannot edit this book.";
        }
    }
}