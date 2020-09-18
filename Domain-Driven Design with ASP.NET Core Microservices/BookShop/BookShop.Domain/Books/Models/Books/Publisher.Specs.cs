namespace BookShop.Domain.Books.Models.Books
{
    using System;
    using Exceptions;
    using FluentAssertions;
    using Xunit;

    public class PublisherSpecs
    {
        [Fact]
        public void ValidPublisherShouldNotThrowException()
        {
            // Act
            Action act = () => new Publisher("Valid name");

            // Assert
            act.Should().NotThrow<InvalidBookException>();
        }

        [Fact]
        public void InvalidNameShouldThrowException()
        {
            // Act
            Action act = () => new Publisher("");

            // Assert
            act.Should().Throw<InvalidBookException>();
        }
    }
}