namespace BookShop.Application.Identity
{
    using Domain.Books.Models.Authors;

    public interface IUser
    {
        void BecomeAuthor(Author author);
    }
}