namespace ShoeTracker.Service.Core.Interfaces
{
    using ShoeTracker.Data.Models.Entities;

    public interface IUserProfileService
    {
        Task<UserProfile?> GetByUserIdAsync(string userId);

        Task CreateOrUpdateAsync(UserProfile profile);
    }
}
