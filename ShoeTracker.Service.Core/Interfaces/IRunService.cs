namespace ShoeTracker.Service.Core.Interfaces
{
    public interface IRunService
    {
        Task AddRunAsync(int shoeId, double distance, string userId);
    }
}
