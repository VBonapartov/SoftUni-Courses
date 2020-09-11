namespace BookShop.Domain.Models
{
    public class ModelConstants
    {
        public class Common
        {
            public const int MinNameLength = 5;
            public const int MaxNameLength = 40;
            public const int MinEmailLength = 3;
            public const int MaxEmailLength = 50;
            public const int MaxUrlLength = 2048;
            public const int Zero = 0;
        }

        public class Options
        {
            public const int MinNumberOfPages = 5;
            public const int MaxNumberOfPages = 1000;
        }

        public class Book
        {
            public const int MinTitleLength = 2;
            public const int MaxTitleLength = 30;
        }

        public class Review
        {
            public const int MinTitleLength = 2;
            public const int MaxTitleLength = 30;
            public const int MinDescriptionLength = 2;
            public const int MaxDescriptionLength = 100;
        }
    }
}