namespace BookShop.Domain.Specifications.Reviews
{
    using System;
    using System.Linq.Expressions;
    using Models.Reviews;

    public class ReviewByTitleSpecification : Specification<Review>
    {
        private readonly string? title;

        public ReviewByTitleSpecification(string? title)
            => this.title = title;

        protected override bool Include => this.title != null;

        public override Expression<Func<Review, bool>> ToExpression()
            => review => review.Title.ToLower().Contains(this.title!.ToLower());
    }
}