namespace BookShop.Domain.Books.Exceptions
{
    using BookShop.Domain.Common;

    public class InvalidOptionsException : BaseDomainException
    {
        public InvalidOptionsException()
        {
        }

        public InvalidOptionsException(string error) => this.Error = error;
    }
}