namespace BookShop.Domain.Books.Specifications.Authors
{
    using System;
    using System.Linq.Expressions;
    using BookShop.Domain.Common;
    using Models.Authors;

    public class AuthorByIdSpecification : Specification<Author>
    {
        private readonly int? id;

        public AuthorByIdSpecification(int? id)
            => this.id = id;

        protected override bool Include => this.id != null;

        public override Expression<Func<Author, bool>> ToExpression()
            => author => author.Id == this.id;
    }
}