namespace ShoeTracker.Service.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using ShoeTracker.Data;
    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Service.Core.Interfaces;

    public class UserProfileService : IUserProfileService
    {
        private readonly ShoeTrackerDbContext _context;

        public UserProfileService(ShoeTrackerDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a user's profile by user ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <returns>The user profile if found, otherwise null </returns>
        public async Task<UserProfile?> GetByUserIdAsync(string userId)
        {
            return await _context.UserProfiles
                .FirstOrDefaultAsync(up => up.UserId == userId);
        }

        /// <summary>
        /// Creates a new user profile or updates an existing one.
        /// </summary>
        /// <param name="profile">The user profile entity to create or update</param>
        public async Task CreateOrUpdateAsync(UserProfile profile)
        {
            UserProfile? existing = await _context.UserProfiles
                .FirstOrDefaultAsync(up => up.UserId == profile.UserId);

            if (existing == null)
            {
                await _context.UserProfiles.AddAsync(profile);
            }

            else
            {
                existing.City = profile.City;
                existing.Bio = profile.Bio;
                existing.YearlyGoal = profile.YearlyGoal;
                _context.UserProfiles.Update(existing);
            }

            await _context.SaveChangesAsync();
        }
    }
}
