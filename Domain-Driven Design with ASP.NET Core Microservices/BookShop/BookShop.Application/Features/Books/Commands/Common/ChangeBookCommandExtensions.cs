namespace BookShop.Application.Features.Books.Commands.Common
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Authors;
    using Contracts;

    internal static class ChangeCarAdCommandExtensions
    {
        public static async Task<Result> AuthorHasBook(
            this ICurrentUser currentUser,
            IAuthorRepository authorRepository,
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