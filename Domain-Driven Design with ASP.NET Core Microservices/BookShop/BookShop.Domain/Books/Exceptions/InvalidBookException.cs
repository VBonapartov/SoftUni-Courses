namespace BookShop.Domain.Books.Exceptions
{
    using BookShop.Domain.Common;

    public class InvalidBookException : BaseDomainException
    {
        public InvalidBookException()
        {
        }

        public InvalidBookException(string error) => this.Error = error;
    }
}