namespace BookShop.Domain.Exceptions
{
    public class InvalidBookException : BaseDomainException
    {
        public InvalidBookException()
        {
        }

        public InvalidBookException(string error) => this.Error = error;
    }
}