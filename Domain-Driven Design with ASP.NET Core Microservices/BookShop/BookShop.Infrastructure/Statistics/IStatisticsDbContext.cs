﻿namespace BookShop.Infrastructure.Statistics
{
    using Common.Persistence;
    using Domain.Statistics.Models;
    using Microsoft.EntityFrameworkCore;

    internal interface IStatisticsDbContext : IDbContext
    {
        DbSet<Statistics> Statistics { get; }

        DbSet<BookView> BookViews { get; }
    }
}
