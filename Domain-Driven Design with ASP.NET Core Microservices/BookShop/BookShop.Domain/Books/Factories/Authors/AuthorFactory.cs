﻿namespace BookShop.Domain.Books.Factories.Authors
{
    using Exceptions;
    using Models.Authors;

    internal class AuthorFactory : IAuthorFactory
    {
        private string name = default!;
        private string authorUserId = default!;

        public IAuthorFactory WithName(string name)
        { 
            this.name = name;
            return this;
        }

        public IAuthorFactory FromUser(string userId)
        {
            this.authorUserId = userId;
            return this;
        }

        public Author Build()
        {
            if (string.IsNullOrEmpty(this.name))
            {
                throw new InvalidAuthorException("Author name must have a value.");
            }

            return new Author(this.name, this.authorUserId);
        }
    }
}