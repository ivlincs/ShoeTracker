namespace ShoeTracker.Tests.Services
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using ShoeTracker.Data;
    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Service.Core.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CommentServiceTests
    {
        private ShoeTrackerDbContext GetContext(string dbName)
        {
            DbContextOptions<ShoeTrackerDbContext> options = new DbContextOptionsBuilder<ShoeTrackerDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new ShoeTrackerDbContext(options);
        }

        [Fact]
        public async Task AddAsync_SavesComment()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(AddAsync_SavesComment));

            ctx.Categories.Add(new Category { Id = 1, Name = "Road" });
            ctx.Shoes.Add(new Shoe
            {
                Id = 1,
                Brand = "Puma",
                Model = "Velocity Nitro 3",

                UserId = "user1",
                CategoryId = 1,
                PurchaseDate = DateTime.Now,
            });
            await ctx.SaveChangesAsync();

            CommentService svc = new CommentService(ctx);

            await svc.AddAsync(1, "Great shoe for tempo runs!", "user1");

            ctx.Comments.Should().HaveCount(1);
            ctx.Comments.First().Content.Should().Be("Great shoe for tempo runs!");
        }

        [Fact]
        public async Task AddAsync_Throws_WhenWrongOwner()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(AddAsync_Throws_WhenWrongOwner));

            ctx.Categories.Add(new Category { Id = 1, Name = "Road" });
            ctx.Shoes.Add(new Shoe
            {
                Id = 1,
                Brand = "Asics",
                Model = "FujiSpeed 3",

                UserId = "user1",
                CategoryId = 1,
                PurchaseDate = DateTime.Now,
            });
            await ctx.SaveChangesAsync();

            CommentService svc = new CommentService(ctx);

            Func<Task> act = async () => await svc.AddAsync(1, "Comment", "user2");

            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task DeleteAsync_RemovesComment()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(DeleteAsync_RemovesComment));

            ctx.Categories.Add(new Category { Id = 1, Name = "Road" });
            ctx.Shoes.Add(new Shoe
            {
                Id = 1,
                Brand = "New Balance",
                Model = "Fuel Cell Summit Unknown v4",
                UserId = "user1",
                CategoryId = 1,
                PurchaseDate = DateTime.Now,
            });
            ctx.Comments.Add(new Comment
            {
                Id = 1,
                ShoeId = 1,
                Content = "Test comment",
                UserId = "user1",
                CreatedOn = DateTime.Now
            });
            await ctx.SaveChangesAsync();

            CommentService svc = new CommentService(ctx);

            await svc.DeleteAsync(1, "user1");

            ctx.Comments.Should().BeEmpty();
        }

        [Fact]
        public async Task DeleteAsync_DoesNotDelete_WhenWrongUser()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(DeleteAsync_DoesNotDelete_WhenWrongUser));

            ctx.Categories.Add(new Category { Id = 1, Name = "Road" });
            ctx.Shoes.Add(new Shoe
            {
                Id = 1,
                Brand = "Asics",
                Model = "FujiLite 4",
                UserId = "user1",
                CategoryId = 1,
                PurchaseDate = DateTime.Now,
            });
            ctx.Comments.Add(new Comment
            {
                Id = 1,
                ShoeId = 1,
                Content = "My comment",
                UserId = "user1",
                CreatedOn = DateTime.Now
            });
            await ctx.SaveChangesAsync();

            CommentService svc = new CommentService(ctx);

            await svc.DeleteAsync(1, "user2");

            ctx.Comments.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetByShoeIdAsync_FiltersCorrectly()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(GetByShoeIdAsync_FiltersCorrectly));

            ctx.Categories.Add(new Category { Id = 1, Name = "Road" });
            ctx.Shoes.AddRange(new Shoe
            {
                Id = 1,
                Brand = "Puma",
                Model = "Velocity Nitro 2",
                UserId = "user1",
                CategoryId = 1,
                PurchaseDate = DateTime.Now,
            },
            new Shoe
            {
                Id = 2,
                Brand = "Asics",
                Model = "Versablast 4",
                UserId = "user1",
                CategoryId = 1,
                PurchaseDate = DateTime.Now,
            });
            ctx.Comments.AddRange(
                new Comment
                {
                    Id = 1,
                    ShoeId = 1,
                    Content = "Comment for Puma",
                    UserId = "user1",
                    CreatedOn = DateTime.Now
                },
                new Comment
                {
                    Id = 2,
                    ShoeId = 2,
                    Content = "Comment for Asics",
                    UserId = "user1",
                    CreatedOn = DateTime.Now
                });
            await ctx.SaveChangesAsync();

            CommentService svc = new CommentService(ctx);

            IEnumerable<Comment>? result = await svc.GetByShoeIdAsync(1);
            result.Should().HaveCount(1);
            result.First().Content.Should().Be("Comment for Puma");
        }
    }
}
