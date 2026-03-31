namespace ShoeTracker.Service.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using ShoeTracker.Data;
    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Service.Core.Interfaces;

    public class CommentService : ICommentService
    {

        private readonly ShoeTrackerDbContext _context;

        public CommentService(ShoeTrackerDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(int shoeId, string content, string userId)
        {
            Shoe? shoe = await _context.Shoes
                .FirstOrDefaultAsync(s => s.Id == shoeId && s.UserId == userId);

            if (shoe == null)
            {
                throw new InvalidOperationException("Shoe not found or acces denied.");
            }

            Comment comment = new Comment
            {
                ShoeId = shoeId,
                Content = content,
                UserId = userId,
                CreatedOn = DateTime.UtcNow
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int commentId, string userId)
        {
            Comment? comment = await _context.Comments
                .Include(c => c.Shoe)
                .FirstOrDefaultAsync(c => c.Id == commentId && c.UserId == userId);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Comment>> GetByShoeIdAsync(int shoeId)
        {
            return await _context.Comments
                .Where(c => c.ShoeId == shoeId)
                .OrderByDescending(c => c.CreatedOn)
                .ToListAsync();
        }
    }
}
