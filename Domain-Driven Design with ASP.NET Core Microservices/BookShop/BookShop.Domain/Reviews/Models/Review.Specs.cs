namespace BookShop.Domain.Reviews.Models.Reviews
{
    using System;
    using Exceptions;
    using FluentAssertions;
    using Xunit;

    public class ReviewSpecs
    {
        [Fact]
        public void ValidReviewShouldNotThrowException()
        {
            // Act
            Action act = () => new Review("123", 1, "Valid title", "Valid description");

            // Assert
            act.Should().NotThrow<InvalidReviewException>();
        }

        [Fact]
        public void InvalidTitleShouldThrowException()
        {
            // Act
            Action act = () => new Review("123", 1, "", "Valid description");

            // Assert
            act.Should().Throw<InvalidReviewException>();
        }

        [Fact]
        public void InvalidDescriptionShouldThrowException()
        {
            // Act
            Action act = () => new Review("123", 1, "Valid title", "");

            // Assert
            act.Should().Throw<InvalidReviewException>();
        }
    }
}