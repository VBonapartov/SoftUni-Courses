namespace BookShop.Domain.Models.Reviews
{
    using System;
    using Exceptions;
    using FluentAssertions;
    using Xunit;

    public class ReviewSpecs
    {
        [Fact]
        public void ValidAuthorShouldNotThrowException()
        {
            // Act
            Action act = () => new Review("Valid title", "Valid description");

            // Assert
            act.Should().NotThrow<InvalidReviewException>();
        }

        [Fact]
        public void InvalidTitleShouldThrowException()
        {
            // Act
            Action act = () => new Review("", "Valid description");

            // Assert
            act.Should().Throw<InvalidReviewException>();
        }

        [Fact]
        public void InvalidDescriptionShouldThrowException()
        {
            // Act
            Action act = () => new Review("Valid title", "");

            // Assert
            act.Should().Throw<InvalidReviewException>();
        }
    }
}