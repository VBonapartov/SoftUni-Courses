namespace BookShop.Domain.Specifications.Books
{
    using System;
    using System.Linq.Expressions;
    using Models.Books;

    public class BookOnlyAvailableSpecification : Specification<Book>
    {
        private readonly bool onlyAvailable;

        public BookOnlyAvailableSpecification(bool onlyAvailable)
            => this.onlyAvailable = onlyAvailable;

        public override Expression<Func<Book, bool>> ToExpression()
        {
            if (this.onlyAvailable)
            {
                return book => book.IsAvailable;
            }

            return book => true;
        }
    }
}