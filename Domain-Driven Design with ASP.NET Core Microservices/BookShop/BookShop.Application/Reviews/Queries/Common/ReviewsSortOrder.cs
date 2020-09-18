namespace BookShop.Application.Reviews.Queries.Common
{
    using System;
    using System.Linq.Expressions;
    using Application.Common;
    using Domain.Reviews.Models;

    public class ReviewsSortOrder : SortOrder<Review>
    {
        public ReviewsSortOrder(string? sortBy, string? order)
            : base(sortBy, order)
        {
        }

        public override Expression<Func<Review, object>> ToExpression()
            => this.SortBy switch
            {
                "title" => review => review.Title,
                "description" => review => review.Description,
                _ => review => review.Id
            };
    }
}