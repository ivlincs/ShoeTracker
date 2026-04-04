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
        /// <summary>
        /// Adds new run to a shoe and updates the shoes`s total distance.
        /// </summary>
        /// <param name="shoeId">The shoe ID to add the run to</param>
        /// <param name="distance">The distance of the run in km</param>
        /// <param name="userId">The user ID to verify shoe ownership</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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
