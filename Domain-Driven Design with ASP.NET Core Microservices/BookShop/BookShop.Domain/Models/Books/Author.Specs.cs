namespace BookShop.Domain.Models.Books
{    
    using System;
    using Exceptions;
    using FluentAssertions;
    using Xunit;

    public class AuthorSpecs
    {
        [Fact]
        public void ValidAuthorShouldNotThrowException()
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