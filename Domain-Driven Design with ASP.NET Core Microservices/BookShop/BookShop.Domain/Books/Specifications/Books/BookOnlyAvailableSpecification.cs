namespace BookShop.Domain.Books.Specifications.Books
{
    using System;
    using System.Linq.Expressions;
    using Domain.Common;
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