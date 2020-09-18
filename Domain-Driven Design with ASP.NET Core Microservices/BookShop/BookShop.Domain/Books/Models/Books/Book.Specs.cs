namespace BookShop.Domain.Books.Models.Books
{    
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public class BookSpecs
    {
        [Fact]
        public void ChangeAvailabilityShouldMutateIsAvailable()
        {
            // Arrange
            var book = A.Dummy<Book>();

            // Act
            book.ChangeAvailability();

            // Assert
            book.IsAvailable.Should().BeFalse();
        }
    }
}