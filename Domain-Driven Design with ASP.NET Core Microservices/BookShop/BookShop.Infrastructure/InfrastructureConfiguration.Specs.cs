namespace BookShop.Infrastructure
{
    using System;
    using System.Reflection;
    using AutoMapper;
    using Books;
    using Common.Events;
    using Common.Persistence;
    using Domain.Books.Repositories;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;    

    public class InfrastructureConfigurationSpecs
    {
        [Fact]
        public void AddRepositoriesShouldRegisterRepositories()
        {
            // Arrange
            var serviceCollection = new ServiceCollection()
                .AddDbContext<BookShopDbContext>(opts => opts
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()))
                .AddScoped<IBooksDbContext>(provider => provider.GetService<BookShopDbContext>())
                .AddTransient<IEventDispatcher, EventDispatcher>();

            // Act
            var services = serviceCollection
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddRepositories()
                .BuildServiceProvider();

            // Assert
            services
                .GetService<IBookDomainRepository>()
                .Should()
                .NotBeNull();
        }
    }
}
