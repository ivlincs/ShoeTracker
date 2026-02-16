namespace ShoeTracker.Service.Core.Interfaces
{
    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Data.Models.Statistics;

    public interface IShoeService
    {
        Task<IEnumerable<Shoe>> GetAllAsync(string userId);

        Task<Shoe?> GetByIdAsync(int id, string userId);

        Task AddAsync(Shoe shoe);

        Task UpdateAsync(Shoe shoe);

        Task DeleteAsync(int id);

        Task<ShoeStatistics> GetStatisticsAsync(string userId);

        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
