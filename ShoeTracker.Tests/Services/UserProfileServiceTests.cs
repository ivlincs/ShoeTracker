namespace ShoeTracker.Tests.Services
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using ShoeTracker.Data;
    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Service.Core.Services;
    using Xunit;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserProfileServiceTests
    {
        private ShoeTrackerDbContext GetContext(string dbName)
        {
            DbContextOptions<ShoeTrackerDbContext> options = new DbContextOptionsBuilder<ShoeTrackerDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new ShoeTrackerDbContext(options);
        }

        [Fact]
        public async Task GetByUserIdAsync_ReturnsNull_WhenNotExists()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(GetByUserIdAsync_ReturnsNull_WhenNotExists));
            UserProfileService svc = new UserProfileService(ctx);

            UserProfile? result = await svc.GetByUserIdAsync("user1");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByUserIdAsync_ReturnsProfile_WhenExists()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(GetByUserIdAsync_ReturnsProfile_WhenExists));

            ctx.UserProfiles.Add(new UserProfile
            {
                UserId = "user1",
                City = "Sofia",
                Bio = "Marathon runner",
                YearlyGoal = 1000,
                CreatedOn = DateTime.Now
            });
            await ctx.SaveChangesAsync();

            UserProfileService svc = new UserProfileService(ctx);

            UserProfile? result = await svc.GetByUserIdAsync("user1");

            result.Should().NotBeNull();
            result!.City.Should().Be("Sofia");
            result.YearlyGoal.Should().Be(1000);
        }

        [Fact]
        public async Task CreateOrUpdateAsync_CreatesProfile()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(CreateOrUpdateAsync_CreatesProfile));
            
            UserProfileService svc = new UserProfileService(ctx);
            UserProfile profile = new UserProfile
            {
                UserId = "user1",
                City = "Plovdiv",
                Bio = "Trail runner",
                YearlyGoal = 800,
                CreatedOn = DateTime.Now
            };
            await svc.CreateOrUpdateAsync(profile);

            ctx.UserProfiles.Should().HaveCount(1);
            ctx.UserProfiles.First().City.Should().Be("Plovdiv");
        }

        [Fact]
        public  async Task CreateOrUpdateAsync_UpdatesExisting()
        {
            ShoeTrackerDbContext ctx = GetContext(nameof(CreateOrUpdateAsync_UpdatesExisting));

            ctx.UserProfiles.Add(new UserProfile
            {
                UserId = "user1",
                City = "Sofia",
                Bio = "Old bio",
                YearlyGoal = 1000,
                CreatedOn = DateTime.Now
            }); 
            await ctx.SaveChangesAsync();

            UserProfileService svc = new UserProfileService(ctx);
            UserProfile updated = new UserProfile
            {
                UserId = "user1",
                City = "Varna",
                Bio = "New bio - ultra runner",
                YearlyGoal = 1500,
                CreatedOn = DateTime.Now
            };
            await svc.CreateOrUpdateAsync(updated); 

            ctx.UserProfiles.Should().HaveCount(1);
            ctx.UserProfiles.First().City.Should().Be("Varna");
            ctx.UserProfiles.First().YearlyGoal.Should().Be(1500);
        }
    }
}

