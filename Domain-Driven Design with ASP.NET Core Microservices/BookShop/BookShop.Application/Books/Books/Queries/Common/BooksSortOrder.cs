namespace BookShop.Application.Books.Books.Queries.Common
{
    using System;
    using System.Linq.Expressions;
    using Application.Common;
    using Domain.Books.Models.Books;

    public class BooksSortOrder : SortOrder<Book>
    {
        public BooksSortOrder(string? sortBy, string? order)
            : base(sortBy, order)
        {
        }

        public override Expression<Func<Book, object>> ToExpression()
            => this.SortBy switch
            {
                "price" => book => book.Price,
                "publisher" => book => book.Publisher.Name,
                _ => book => book.Id
            };
    }
}