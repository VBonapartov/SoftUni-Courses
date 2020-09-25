namespace BookShop.Domain.Books.Models.Books
{
    using System;
    using System.Collections.Generic;
    using Common;

    internal class PublisherData : IInitialData
    {
        public Type EntityType => typeof(Publisher);

        public IEnumerable<object> GetData()
            => new List<Publisher>
            {
                new Publisher("Penguin Random House"),
                new Publisher("Hachette Livre"),
                new Publisher("HarperCollins"),
                new Publisher("Macmillan ")
            };
    }
}
