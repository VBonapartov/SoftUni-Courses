namespace BookShop.Domain.Models.Books
{
    using Common;
    using Exceptions;

    using static ModelConstants.Common;

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