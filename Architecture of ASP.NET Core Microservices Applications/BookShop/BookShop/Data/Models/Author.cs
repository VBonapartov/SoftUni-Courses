namespace BookShop.Data.Models
{
    using System.Collections.Generic;

    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}