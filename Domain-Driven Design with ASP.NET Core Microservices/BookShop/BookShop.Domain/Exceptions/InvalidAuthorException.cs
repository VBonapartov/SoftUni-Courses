namespace BookShop.Domain.Exceptions
{
    public class InvalidAuthorException : BaseDomainException
    {
        public InvalidAuthorException()
        {
        }

        public InvalidAuthorException(string error) => this.Error = error;
    }
}