namespace BookShop.Domain.Books.Models.Books
{
    using Common.Models;

    public class CoverType : Enumeration
    {
        public static readonly CoverType Hardcover = new CoverType(1, nameof(Hardcover));
        public static readonly CoverType Softcover = new CoverType(2, nameof(Softcover));

        private CoverType(int value)
            : this(value, FromValue<CoverType>(value).Name)
        {
        }

        private CoverType(int value, string name)
            : base(value, name)
        {
        }
    }
}