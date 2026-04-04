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

        /// <summary>
        /// Adds a new comment to a shoe.
        /// </summary>
        /// <param name="shoeId">The shoe ID to add the comment to.</param>
        /// <param name="content">The comment text content</param>
        /// <param name="userId">The user Id to verify shoe ownership</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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

        /// <summary>
        /// Delete a comment from the database.
        /// </summary>
        /// <param name="commentId">The comment ID to delete</param>
        /// <param name="userId">The user ID to verify comment ownership</param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieves all comments for a specifics shoe.
        /// </summary>
        /// <param name="shoeId">The shoe ID to retrieve comments for</param>
        /// <returns>A collection of comments ordered by creation date (newest first).</returns>
        public async Task<IEnumerable<Comment>> GetByShoeIdAsync(int shoeId)
        {
            return await _context.Comments
                .Where(c => c.ShoeId == shoeId)
                .OrderByDescending(c => c.CreatedOn)
                .ToListAsync();
        }
    }
}
