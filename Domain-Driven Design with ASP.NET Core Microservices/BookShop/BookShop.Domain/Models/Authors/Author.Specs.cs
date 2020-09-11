namespace BookShop.Domain.Models.Authors
{    
    using System;
    using Books;
    using Exceptions;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public class AuthorSpecs
    {
        [Fact]
        public void ValidAuthorShouldNotThrowException()
        {
            // Act
            Action act = () => new Author("Valid name");

            // Assert
            act.Should().NotThrow<InvalidAuthorException>();
        }

        [Fact]
        public void InvalidNameShouldThrowException()
        {
            // Act
            Action act = () => new Author("");

            // Assert
            act.Should().Throw<InvalidAuthorException>();
        }

        [Fact]
        public void AddBookShouldSaveBook()
        {
            // Arrange
            var author = new Author("Valid author");
            var book = A.Dummy<Book>();

            // Act
            author.AddBook(book);

            // Assert
            author.Books.Should().Contain(book);
        }
    }
}