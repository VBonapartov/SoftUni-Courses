namespace BookShop.Domain.Reviews.Exceptions
{
    using BookShop.Domain.Common;

    public class InvalidReviewException : BaseDomainException
    {
        public InvalidReviewException()
        {
        }

        public InvalidReviewException(string error) => this.Error = error;
    }
}