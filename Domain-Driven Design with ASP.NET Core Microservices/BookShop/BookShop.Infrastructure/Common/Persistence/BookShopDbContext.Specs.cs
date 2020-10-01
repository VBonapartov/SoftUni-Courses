namespace BookShop.Infrastructure.Common.Persistence
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Statistics.Handlers;
    using AutoMapper;
    using Domain.Books.Events.Books;
    using Domain.Books.Models.Authors;
    using Domain.Statistics.Models;
    using Infrastructure.Books;
    using Events;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Statistics;
    using Xunit;   

    public class CarRentalDbContextSpecs
    {
        [Fact]
        public async Task RaisedEventsShouldBeHandled()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddDbContext<BookShopDbContext>(opts => opts
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()))
                .AddScoped<IBooksDbContext>(provider => provider
                    .GetService<BookShopDbContext>())
                .AddScoped<IStatisticsDbContext>(provider => provider
                    .GetService<BookShopDbContext>())
                .AddTransient<IEventDispatcher, EventDispatcher>()
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddTransient<IEventHandler<BookAddedEvent>, BookAddedEventHandler>()
                .AddRepositories()
                .BuildServiceProvider();

            var author = AuthorFakes.Data.GetAuthor();
            var dbContext = services.GetService<BookShopDbContext>();

            var statisticsToAdd = new StatisticsData()
                .GetData()
                .First();

            dbContext.Add(statisticsToAdd);
            await dbContext.SaveChangesAsync();

            // Act
            dbContext.Authors.Add(author);
            await dbContext.SaveChangesAsync();

            // Assert
            var statistics = await dbContext.Statistics.SingleAsync();

            statistics.TotalBooks.Should().Be(10);
        }
    }
}
