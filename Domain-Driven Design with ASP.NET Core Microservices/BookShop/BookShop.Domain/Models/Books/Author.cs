namespace BookShop.Domain.Models.Books
{
    using Common;
    using Exceptions;

    using static ModelConstants.Common;

    public class Author : Entity<int>
    {
        internal Author(string name)
        {
            this.Validate(name);

            this.Name = name;
        }

        public string Name { get; private set; }

        public Author UpdateName(string name)
        {
            this.Validate(name);
            this.Name = name;

            return this;
        }

        public void Validate(string newName)
            => Guard.ForStringLength<InvalidBookException>(
                newName,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));
    }
}