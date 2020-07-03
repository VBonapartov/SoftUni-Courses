namespace BookShop.Books.Data.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CategoryBook> Books { get; set; } = new List<CategoryBook>();
    }
}