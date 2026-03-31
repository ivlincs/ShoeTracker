namespace ShoeTracker.Service.Core.Interfaces
{
    using ShoeTracker.Data.Models.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task AddAsync(int shoeId, string content, string userId);

        Task DeleteAsync(int commentId, string userId);

        Task<IEnumerable<Comment>> GetByShoeIdAsync(int shoeId);
    }
}
