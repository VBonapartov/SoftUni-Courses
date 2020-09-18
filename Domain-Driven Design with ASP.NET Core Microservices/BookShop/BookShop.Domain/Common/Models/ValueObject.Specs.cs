namespace BookShop.Domain.Common.Models
{
    using BookShop.Domain.Books.Models.Books;
    using FluentAssertions;
    using Xunit;

    public class ValueObjectSpecs
    {
        [Fact]
        public void ValueObjectsWithEqualPropertiesShouldBeEqual()
        {
            // Arrange
            var first = new Options(10, CoverType.Hardcover, CategoryType.Mystery);
            var second = new Options(10, CoverType.Hardcover, CategoryType.Mystery);

            // Act
            var result = first == second;

            // Arrange
            result.Should().BeTrue();
        }

        [Fact]
        public void ValueObjectsWithDifferentPropertiesShouldNotBeEqual()
        {
            // Arrange
            var first = new Options(10, CoverType.Hardcover, CategoryType.Mystery);
            var second = new Options(10, CoverType.Hardcover, CategoryType.Horror);

            // Act
            var result = first == second;

            // Arrange
            result.Should().BeFalse();
        }
    }
}