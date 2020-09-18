namespace BookShop.Domain.Books.Specifications.Books
{
    using System;
    using System.Linq.Expressions;
    using BookShop.Domain.Common;
    using Models.Books;

    public class BookByPublisherSpecification : Specification<Book>
    {
        private readonly string? publisher;

        public BookByPublisherSpecification(string? publisher)
            => this.publisher = publisher;

        protected override bool Include => this.publisher != null;

        public override Expression<Func<Book, bool>> ToExpression()
            => book => book.Publisher.Name.ToLower()
                .Contains(this.publisher!.ToLower());
    }
}