namespace BookShop.Domain.Books.Exceptions
{
    using BookShop.Domain.Common;

    public class InvalidAuthorException : BaseDomainException
    {
        public InvalidAuthorException()
        {
        }

        public InvalidAuthorException(string error) => this.Error = error;
    }
}