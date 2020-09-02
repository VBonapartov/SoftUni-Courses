namespace BookShop.Domain.Models.Books
{    
    using FakeItEasy;
    using FluentAssertions;
    using Reviews;
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

        [Fact]
        public void AddReviewShouldSaveReview()
        {
            // Arrange
            var review = new Review("Valid title", "Valid description");
            var book = A.Dummy<Book>();

            // Act
            book.AddReview(review);

            // Assert
            book.Reviews.Should().Contain(review);
        }
    }
}