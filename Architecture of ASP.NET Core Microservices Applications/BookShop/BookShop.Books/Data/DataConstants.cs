namespace BookShop.Books.Data
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
            public const int MinBookNameLength = 5;
            public const int MaxBookNameLength = 100;
        }

        public class User
        {
            public const int MinEmailLength = 3;
            public const int MaxEmailLength = 50;
        }
    }
}