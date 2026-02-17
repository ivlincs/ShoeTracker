namespace ShoeTracker.Service.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using ShoeTracker.Data;
    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Service.Core.Interfaces;

    public class RunService : IRunService
    {
        private readonly ShoeTrackerDbContext _context;

        public RunService(ShoeTrackerDbContext context)
        {
            _context = context;
        }
        public async Task AddRunAsync(int shoeId, double distance, string userId)
        {
            Shoe? shoe = await _context.Shoes
                .FirstOrDefaultAsync(s => s.Id == shoeId && s.UserId == userId);

            if (shoe == null)
            {
                throw new InvalidOperationException("Shoe not found");
            }

            Run run = new Run
            {
                ShoeId = shoeId,
                Distance = distance,
                Date = DateTime.Now,
                UserId = userId
            };

            shoe.TotalDistance += distance;

            await _context.Runs.AddAsync(run);
            await _context.SaveChangesAsync();
        }
    }    
}
