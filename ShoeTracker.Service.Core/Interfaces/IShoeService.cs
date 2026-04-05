namespace ShoeTracker.Service.Core.Interfaces
{
    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Data.Models.Statistics;
    using ShoeTracker.Common;

    public interface IShoeService
    {
        Task<IEnumerable<Shoe>> GetAllAsync(string userId);

        Task<Shoe?> GetByIdAsync(int id, string userId);

        Task AddAsync(Shoe shoe);

        Task UpdateAsync(Shoe shoe);

        Task DeleteAsync(int id);

        Task ArchiveAsync(int id);

        Task<ShoeStatistics> GetStatisticsAsync(string userId);

        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<IEnumerable<Shoe>> GetArchivedAsync(string userId);

        Task<PaginatedList<Shoe>> GetPaginatedAsync(string userId, int pageIndex, int pageSize);

        Task<PaginatedList<Shoe>> SearchAsync(string userId, string searchTerm, int pageIndex, int pageSize);
    }
}
