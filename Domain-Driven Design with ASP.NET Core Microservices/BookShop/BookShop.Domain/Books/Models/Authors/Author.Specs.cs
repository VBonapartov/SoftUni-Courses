﻿namespace BookShop.Domain.Books.Models.Authors
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
            Action act = () => new Author("Valid name", "TestUserId");

            // Assert
            act.Should().NotThrow<InvalidAuthorException>();
        }

        [Fact]
        public void InvalidNameShouldThrowException()
        {
            // Act
            Action act = () => new Author("", "TestUserId");

            // Assert
            act.Should().Throw<InvalidAuthorException>();
        }

        [Fact]
        public void AddBookShouldSaveBook()
        {
            // Arrange
            var author = new Author("Valid author", "TestUserId");
            var book = A.Dummy<Book>();

            // Act
            author.AddBook(book);

            // Assert
            author.Books.Should().Contain(book);
        }
    }
}