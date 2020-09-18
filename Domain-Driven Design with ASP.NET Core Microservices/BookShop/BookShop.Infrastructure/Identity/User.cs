namespace BookShop.Infrastructure.Identity
{
    using Application.Identity;
    using Domain.Books.Exceptions;
    using Domain.Books.Models.Authors;    
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser, IUser
    {
        internal User(string email)
            : base(email)
            => this.Email = email;

        public Author? Author { get; private set; }

        public void BecomeAuthor(Author author)
        {
            if (this.Author != null)
            {
                throw new InvalidAuthorException($"User '{this.UserName}' is already a author.");
            }

            this.Author = author;
        }
    }
}