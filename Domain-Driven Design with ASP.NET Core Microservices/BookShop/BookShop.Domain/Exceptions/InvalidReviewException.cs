namespace BookShop.Domain.Exceptions
{
    public class InvalidReviewException : BaseDomainException
    {
        public InvalidReviewException()
        {
        }

        public InvalidReviewException(string error) => this.Error = error;
    }
}