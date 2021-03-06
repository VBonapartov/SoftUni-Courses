﻿namespace BookShop.Domain.Books.Models.Books
{
    using Common.Models;
    using Exceptions;

    using static Common.Models.ModelConstants.Options;

    public class Options : ValueObject
    {
        internal Options(int numberOfPages, CoverType coverType, CategoryType categoryType)
        {
            this.Validate(numberOfPages);

            this.NumberOfPages = numberOfPages;
            this.CoverType = coverType;
            this.CategoryType = categoryType;
        }

        private Options(int numberOfPages)
        {
            this.NumberOfPages = numberOfPages;
            this.CoverType = default!;
            this.CategoryType = default!;
        }

        public int NumberOfPages { get; }

        public CoverType CoverType { get; }

        public CategoryType CategoryType { get; }

        private void Validate(int numberOfSeats)
            => Guard.AgainstOutOfRange<InvalidOptionsException>(
                numberOfSeats,
                MinNumberOfPages,
                MaxNumberOfPages,
                nameof(this.NumberOfPages));
    }
}