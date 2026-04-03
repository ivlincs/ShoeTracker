namespace ShoeTracker.Tests.Services
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using ShoeTracker.Common;
    using ShoeTracker.Data;
    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Data.Models.Statistics;
    using ShoeTracker.Service.Core.Services;
    using System;
    using Xunit;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoeServiceTests
    {
        private ShoeTrackerDbContext GetContext(string dbName)

        {
            DbContextOptions<ShoeTrackerDbContext> options = new DbContextOptionsBuilder<ShoeTrackerDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return new ShoeTrackerDbContext(options);
        }


        [Fact]
        public async Task GetAllAsync_ReturnOnlyOwnersShoe()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(GetAllAsync_ReturnOnlyOwnersShoe));

            ctx.Categories.Add(
                new Category
                {
                    Id = 1,
                    Name = "Road"
                });

            ctx.Shoes.AddRange(
                new Shoe
                {
                    Id = 1,
                    Brand = "Asics",
                    Model = "Versablast 4"
                                           ,
                    UserId = "user1",
                    CategoryId = 1,
                    PurchaseDate = DateTime.Now
                },
                new Shoe
                {
                    Id = 2,
                    Brand = "Puma",
                    Model = "Velocity Nitro 2"
                         ,
                    UserId = "user2",
                    CategoryId = 1,
                    PurchaseDate = DateTime.Now
                });

            await ctx.SaveChangesAsync();

            ShoeService svc = new ShoeService(ctx);

            IEnumerable<Shoe>? result = await svc.GetAllAsync("user1");

            result.Should().HaveCount(1);
            result.First().Brand.Should().Be("Asics");
        }

        [Fact]
        public async Task GetByIdAsync_ReturnNull_WhenWrongUser()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(GetByIdAsync_ReturnNull_WhenWrongUser));
            ctx.Categories.Add(
                new Category
                {
                    Id = 1,
                    Name = "Road"
                });
            ctx.Shoes.Add(
                new Shoe
                {
                    Id = 1,
                    Brand = "Asics",
                    Model = "FujiSpeed 3"
                                           ,
                    UserId = "user1",
                    CategoryId = 1,
                    PurchaseDate = DateTime.Now
                });
            await ctx.SaveChangesAsync();

            ShoeService svc = new ShoeService(ctx);

            Shoe? result = await svc.GetByIdAsync(1, "user2");
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnShoe_WhenCorrectUser()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(GetByIdAsync_ReturnShoe_WhenCorrectUser));
            ctx.Categories.Add(
                new Category
                {
                    Id = 1,
                    Name = "Road"
                });
            ctx.Shoes.Add(
                new Shoe
                {
                    Id = 1,
                    Brand = "New Balance",
                    Model = "Fresh Foam Garoe"
                                           ,
                    UserId = "user1",
                    CategoryId = 1,
                    PurchaseDate = DateTime.Now
                });
            await ctx.SaveChangesAsync();

            ShoeService svc = new ShoeService(ctx);

            Shoe? result = await svc.GetByIdAsync(1, "user1");

            result.Should().NotBeNull();
            result!.Model.Should().Be("Fresh Foam Garoe");
        }

        [Fact]
        public async Task AddAsync_PersistsShoe()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(AddAsync_PersistsShoe));
            ctx.Categories.Add(
                new Category
                {
                    Id = 1,
                    Name = "Road"
                });

            await ctx.SaveChangesAsync();
            ShoeService svc = new ShoeService(ctx);

            await svc.AddAsync(new Shoe
            {
                Brand = "Asics",
                Model = "Versablast 4"
                                           ,
                UserId = "user1",
                CategoryId = 1,
                PurchaseDate = DateTime.Now
            });

            ctx.Shoes.Should().HaveCount(1);

        }

        [Fact]
        public async Task DeleteAsync_RemovesShoe()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(DeleteAsync_RemovesShoe));
            ctx.Categories.Add(
                new Category
                {
                    Id = 1,
                    Name = "Road"
                });
            ctx.Shoes.Add(
                new Shoe
                {
                    Id = 1,
                    Brand = "Puma",
                    Model = "Puma Velocity Nitro 3",

                    UserId = "user1",
                    CategoryId = 1,
                    PurchaseDate = DateTime.Now
                });
            await ctx.SaveChangesAsync();

            ShoeService svc = new ShoeService(ctx);

            await svc.DeleteAsync(1);

            ctx.Shoes.Should().BeEmpty();
        }

        [Fact]
        public async Task GetStatisticsAsync_CountsCorrectly()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(GetStatisticsAsync_CountsCorrectly));
                ctx.Categories .Add(new Category
                {
                    Id = 1,
                    Name = "Road"
                });
            ctx.Shoes.AddRange(
                new Shoe
                {
                    Id = 1,
                    Brand = "New Balance",
                    Model = "Fuel Cell Summit Unknown v4",
                    UserId = "user1",
                    CategoryId = 1,
                    PurchaseDate = DateTime.Now,
                    TotalDistance = 300
                },
                new Shoe
                {
                    Id = 2,
                    Brand = "Asics",
                    Model = "FujiSpeed 3",
                    UserId = "user1",
                    CategoryId = 1,
                    PurchaseDate = DateTime.Now,
                    TotalDistance = 700
                });
            await ctx.SaveChangesAsync();

            ShoeService svc = new ShoeService(ctx);

            ShoeStatistics? stats = await svc.GetStatisticsAsync("user1");

            stats.TotalShoes.Should().Be(2);
            stats.TotalDistance.Should().Be(1000);
            stats.ShoesNeedingReplacement.Should().Be(1);
        }

        [Fact]
        public async Task SearchAsync_FiltersByBrand()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(SearchAsync_FiltersByBrand));
            ctx.Categories.Add(
                new Category
                {
                    Id = 1,
                    Name = "Road"
                });
            ctx.Shoes.AddRange(
                new Shoe
                {
                    Id = 1,
                    Brand = "Asics",
                    Model = "FujiLite 4",

                    UserId = "user1",
                    CategoryId = 1,
                    PurchaseDate = DateTime.Now
                },
                new Shoe
                {
                    Id = 2,
                    Brand = "Puma",
                    Model = "Velocity Nitro 2",
                    UserId = "user1",
                    CategoryId = 1,
                    PurchaseDate = DateTime.Now
                });

            await ctx.SaveChangesAsync();
            ShoeService svc = new ShoeService(ctx);

            PaginatedList<Shoe>? result = await svc.SearchAsync("user1", "Asics", 1, 10);

            result.Should().HaveCount(1);
            result.First().Brand.Should().Be("Asics");
        }
    }
}
