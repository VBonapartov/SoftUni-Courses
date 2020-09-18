namespace BookShop.Domain.Books.Models.Books
{
    using Common.Models;

    public class CategoryType : Enumeration
    {
        public static readonly CategoryType Fantasy = new CategoryType(1, nameof(Fantasy));
        public static readonly CategoryType Adventure = new CategoryType(2, nameof(Adventure));
        public static readonly CategoryType Romance = new CategoryType(3, nameof(Romance));
        public static readonly CategoryType Contemporary = new CategoryType(4, nameof(Contemporary));
        public static readonly CategoryType Dystopian = new CategoryType(5, nameof(Dystopian));
        public static readonly CategoryType Mystery = new CategoryType(6, nameof(Mystery));
        public static readonly CategoryType Horror = new CategoryType(7, nameof(Horror));

        private CategoryType(int value)
            : this(value, FromValue<CoverType>(value).Name)
        {
        }

        private CategoryType(int value, string name)
            : base(value, name)
        {
        }
    }
}