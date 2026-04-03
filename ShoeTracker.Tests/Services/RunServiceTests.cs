namespace ShoeTracker.Tests.Services
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using ShoeTracker.Data;
    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Service.Core.Services;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class RunServiceTests
    {
        private ShoeTrackerDbContext GetContext(string dbName)
        {
            DbContextOptions<ShoeTrackerDbContext> options = new DbContextOptionsBuilder<ShoeTrackerDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new ShoeTrackerDbContext(options);
        }

        [Fact]
        public async Task AddRunAsync_UpdatesTotalDistance()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(AddRunAsync_UpdatesTotalDistance));

            ctx.Categories.Add(new Category { Id = 1, Name = "Running" });
            ctx.Shoes.Add(new Shoe
            {
                Id = 1,
                Brand = "Asics",
                Model = "FujiLite 4",

                UserId = "user1",
                CategoryId = 1,
                PurchaseDate = DateTime.Now,
                TotalDistance = 50
            });
            await ctx.SaveChangesAsync();

            RunService svc = new RunService(ctx);

            await svc.AddRunAsync(1, 10.5, "user1");

            Shoe? shoe = await ctx.Shoes.FindAsync(1);
            shoe!.TotalDistance.Should().Be(60.5);
        }

        [Fact]
        public async Task AddRunAsync_CreatesRunRecord()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(AddRunAsync_CreatesRunRecord));

            ctx.Categories.Add(new Category { Id = 1, Name = "Running" });
            ctx.Shoes.Add(new Shoe
            {
                Id = 1,
                Brand = "Puma",
                Model = "Velocity Nitro 2",
                UserId = "user1",
                CategoryId = 1,
                PurchaseDate = DateTime.Now,
                TotalDistance = 0
            });
            await ctx.SaveChangesAsync();

            RunService svc = new RunService(ctx);

            await svc.AddRunAsync(1, 5.0, "user1");

            ctx.Runs.Should().HaveCount(1);
            ctx.Runs.First().Distance.Should().Be(5.0);
        }

        [Fact]
        public async Task AddRunAsync_Throws_WhenShoeNotFound()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(AddRunAsync_Throws_WhenShoeNotFound));

            RunService svc = new RunService(ctx);

            Func<Task> act = async () => await svc.AddRunAsync(999, 5.0, "user1");

            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task AddRunAsync_Throws_WhenShoeOwnedByOther()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(AddRunAsync_Throws_WhenShoeOwnedByOther));

            ctx.Categories.Add(new Category { Id = 1, Name = "Road" });
            ctx.Shoes.Add(new Shoe
            {
                Id = 1,
                Brand = "New Balance",
                Model = "Fresh Foam Garoe",

                UserId = "user1",
                CategoryId = 1,
                PurchaseDate = DateTime.Now,
                TotalDistance = 0
            });
            await ctx.SaveChangesAsync();

            RunService svc = new RunService(ctx);

            Func<Task> act = async () => await svc.AddRunAsync(1, 5.0, "user2");

            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task AddRunAsync_SetsCorrectUserId()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(AddRunAsync_SetsCorrectUserId));

            ctx.Categories.Add(new Category { Id = 1, Name = "Road" });
            ctx.Shoes.Add(new Shoe
            {
                Id = 1,
                Brand = "Asics",
                Model = "Versablast 4",

                UserId = "user1",
                CategoryId = 1,
                PurchaseDate = DateTime.Now,
                TotalDistance = 0

            });
            await ctx.SaveChangesAsync();

            RunService svc = new RunService(ctx);

            await svc.AddRunAsync( 1, 8.0, "user1");

            ctx.Runs.First().UserId.Should().Be("user1");   
        }
    }
}
 