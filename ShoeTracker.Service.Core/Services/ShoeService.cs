namespace ShoeTracker.Service.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ShoeTracker.Data;
    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Data.Models.Statistics;
    using ShoeTracker.Service.Core.Interfaces;

    public class ShoeService : IShoeService
    {
        private readonly ShoeTrackerDbContext _context;

        public ShoeService(ShoeTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shoe>> GetAllAsync(string userId)
        {
            return await _context.Shoes
                .Where(s => s.UserId == userId)
                .Include(s => s.Runs)
                .Include(s => s.Category)
                .OrderByDescending(s => s.PurchaseDate)
                .ToListAsync();
        }

        public async Task<Shoe?> GetByIdAsync(int id, string userId)
        {
            return await _context.Shoes
                .Include(s => s.Runs)
                .Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
        }

        public async Task AddAsync(Shoe shoe)
        {
            await _context.Shoes.AddAsync(shoe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Shoe shoe)
        {
            _context.Shoes.Update(shoe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Shoe? shoe = await _context.Shoes.FindAsync(id);
            if (shoe != null)
            {
                _context.Shoes.Remove(shoe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ShoeStatistics> GetStatisticsAsync(string userId)
        {
            List<Shoe>? shoes = await _context.Shoes
                .Where(s => s.UserId == userId)
                .Include(s => s.Runs)
                .ToListAsync();

            return new ShoeStatistics
            {
                TotalShoes = shoes.Count,
                TotalDistance = shoes.Sum(s => s.TotalDistance),
                TotalRuns = shoes.Sum(s => s.Runs.Count),
                ShoesNeedingReplacement = shoes.Count(s => s.TotalDistance >= 600),
                AverageDistancePerShoe = shoes.Any() ? shoes.Average(s => s.TotalDistance) : 0,
            };
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .ToListAsync();
        }
    }
}
