namespace BookShop.Domain.Factories.Authors
{
    using Exceptions;
    using Models.Authors;

    internal class AuthorFactory : IAuthorFactory
    {
        private string name = default!;

        public IAuthorFactory WithName(string name)
        { 
            this.name = name;
            return this;
        }

        public Author Build()
        {
            if (string.IsNullOrEmpty(this.name))
            {
                throw new InvalidAuthorException("Author name must have a value.");
            }

            return new Author(this.name);
        }
    }
}