namespace BookShop.Domain.Books.Models.Books
{
    using System;
    using Exceptions;
    using FluentAssertions;
    using Xunit;

    public class OptionsSpecs
    {
        [Fact]
        public void ValidOptionsShouldNotThrowException()
        {
            // Act
            Action act = () => new Options(100, CoverType.Hardcover, CategoryType.Mystery);

            // Assert
            act.Should().NotThrow<InvalidOptionsException>();
        }

        [Fact]
        public void InvalidNumberOfPagesShouldThrowException()
        {
            // Act
            Action act = () => new Options(0, CoverType.Hardcover, CategoryType.Mystery);

            // Assert
            act.Should().Throw<InvalidOptionsException>();
        }
    }
}