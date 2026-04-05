namespace ShoeTracker.Service.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ShoeTracker.Data;
    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Data.Models.Statistics;
    using ShoeTracker.Service.Core.Interfaces;
    using ShoeTracker.Common;

    public class ShoeService : IShoeService
    {
        private readonly ShoeTrackerDbContext _context;
        private const double REPLACEMENT_THRESHOLD_KM = 600;

        public ShoeService(ShoeTrackerDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all shoes belonging to a specific user.
        /// </summary>
        /// <param name="userId"> The unique identifier of the user </param>
        /// <returns> A collection of shoe owned by the user </returns>
        public async Task<IEnumerable<Shoe>> GetAllAsync(string userId)
        {
            return await _context.Shoes
                .Where(s => s.UserId == userId && !s.IsArchived)
                .Include(s => s.Runs)
                .Include(s => s.Category)
                .OrderByDescending(s => s.PurchaseDate)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves specific shoe by ID,ensure user owns it
        /// </summary>
        /// <param name="id">The shoe ID</param>
        /// <param name="userId">The user Id to verify ownership</param>
        /// <returns>The shoe if found and owned by the user, otherwise null</returns>
        public async Task<Shoe?> GetByIdAsync(int id, string userId)
        {
            return await _context.Shoes
                .Include(s => s.Runs)
                .Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
        }

        /// <summary>
        /// Adds new shoe to the database.
        /// </summary>
        /// <param name="shoe">The shoe entity to add.</param>
        public async Task AddAsync(Shoe shoe)
        {
            await _context.Shoes.AddAsync(shoe);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing shoe in the database.
        /// </summary>
        /// <param name="shoe">The shoe entity with updated value</param>
        public async Task UpdateAsync(Shoe shoe)
        {
            _context.Shoes.Update(shoe);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a shoe from the database.
        /// </summary>
        /// <param name="id">The Id of the shoe to delete</param>
        public async Task DeleteAsync(int id)
        {
            Shoe? shoe = await _context.Shoes.FindAsync(id);
            if (shoe != null)
            {
                _context.Shoes.Remove(shoe);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Calculates statistics for a user`s shoe collection.
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <returns>Statistics including total shoes, distance and replacement need</returns>
        public async Task<ShoeStatistics> GetStatisticsAsync(string userId)
        {
            List<Shoe>? shoes = await _context.Shoes
                .Where(s => s.UserId == userId && !s.IsArchived)
                .Include(s => s.Runs)
                .ToListAsync();

            return new ShoeStatistics
            {
                TotalShoes = shoes.Count,
                TotalDistance = shoes.Sum(s => s.TotalDistance),
                TotalRuns = shoes.Sum(s => s.Runs.Count),
                ShoesNeedingReplacement = shoes.Count(s => s.TotalDistance >= REPLACEMENT_THRESHOLD_KM),
                AverageDistancePerShoe = shoes.Any() ? shoes.Average(s => s.TotalDistance) : 0,
            };
        }

        /// <summary>
        /// Retrieves all available shoe categories.
        /// </summary>
        /// <returns>A collection of all categories</returns>
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a paginated list of shoes for a user.
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="pageIndex">The page number (1-based)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>A paginated list of shoes</returns>
        public async Task<PaginatedList<Shoe>> GetPaginatedAsync(string userId, int pageIndex, int pageSize)
        {
            IQueryable<Shoe> query = _context.Shoes
                .Where(s => s.UserId == userId && !s.IsArchived)
                .Include(s => s.Runs)
                .Include(s => s.Category)
                .OrderByDescending(s => s.PurchaseDate);

            return await PaginatedList<Shoe>.CreateAsync(query, pageIndex, pageSize);
        }

        /// <summary>
        /// Search for shoe by brand,model or category name.
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="searchTerm">The search term to filter by.</param>
        /// <param name="pageIndex">The page number</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>A paginated list of matching shoes</returns>
        public async Task<PaginatedList<Shoe>> SearchAsync(string userId, string searchTerm, int pageIndex, int pageSize)
        {
            IQueryable<Shoe> query = _context.Shoes
                .Where(s => s.UserId == userId && !s.IsArchived)
                .Include(s => s.Runs)
                .Include(s => s.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string term = searchTerm.ToLower();

                query = query.Where(s =>
                    s.Brand.ToLower().Contains(term) ||
                    s.Model.ToLower().Contains(term) ||
                    s.Category.Name.ToLower().Contains(term)
                );
            }

            return await PaginatedList<Shoe>.CreateAsync(query.OrderByDescending(s => s.PurchaseDate), pageIndex, pageSize);
        }

        /// <summary>
        /// Archives a shoe instead of deleting it (soft delete).    
        /// </summary>
        /// <param name="id">The shoe ID to archive.</param>
        public async Task ArchiveAsync(int id)
        {
            Shoe? shoe = await _context.Shoes.FindAsync(id);
            if (shoe != null)
            {
                shoe.IsArchived = true;
                _context.Shoes.Update(shoe);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrivesa ll archived shoes for user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A collection of archived shoes.</returns>
        public async Task<IEnumerable<Shoe>> GetArchivedAsync(string userId)
        {
            return await _context.Shoes
                .Where(s => s.UserId == userId && s.IsArchived)
                .Include(s => s.Category)
                .Include(s => s.Runs)
                .OrderByDescending(s => s.PurchaseDate)
                .ToListAsync();
        }
    }
}
