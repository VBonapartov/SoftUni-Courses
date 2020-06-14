namespace BookShop.Data
{
    public class DataConstants
    {
        public class Author
        {
            public const int MinFirstNameLength = 2;
            public const int MaxFirstNameLength = 50;
            public const int MinLastNameLength = 2;
            public const int MaxLastNameLength = 50;
        }

        public class Category
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
        }

        public class Book
        {
            public const int MinTitleLength = 5;
            public const int MaxTitleLength = 100;
            public const int MinDescriptionLength = 5;
            public const int MaxDescriptionLength = 300;
        }

        public class Review
        {
            public const int MinTitleLength = 5;
            public const int MaxTitleLength = 30;
            public const int MinDescriptionLength = 5;
            public const int MaxDescriptionLength = 2000;
            public const int MinAuthorLength = 2;
            public const int MaxAuthorLength = 50;
        }
    }
}