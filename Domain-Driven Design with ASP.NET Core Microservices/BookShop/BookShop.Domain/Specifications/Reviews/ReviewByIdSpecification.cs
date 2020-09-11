namespace BookShop.Domain.Specifications.Reviews
{
    using System;
    using System.Linq.Expressions;
    using Models.Reviews;

    public class ReviewByIdSpecification : Specification<Review>
    {
        private readonly int? id;

        public ReviewByIdSpecification(int? id)
            => this.id = id;

        protected override bool Include => this.id != null;

        public override Expression<Func<Review, bool>> ToExpression()
            => review => review.Id == this.id;
    }
}