namespace BookShop.Domain.Books.Models.Books
{
    using Common.Models;
    using Exceptions;

    using static Common.Models.ModelConstants.Common;

    public class Publisher : Entity<int>
    {
        internal Publisher(string name)
        {
            this.Validate(name);

            this.Name = name;
        }

        public string Name { get; private set; }

        public Publisher UpdateName(string name)
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