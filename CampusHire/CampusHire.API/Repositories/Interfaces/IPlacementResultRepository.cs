using CampusHire.API.Entities;
namespace CampusHire.API.Repositories.Interfaces
{
    public interface IPlacementResultRepository
    {
        Task<List<PlacementResult>> GetAllAsync();
        Task<PlacementResult?> GetByIdAsync(int id);
        Task AddAsync(PlacementResult result);
        Task UpdateAsync(PlacementResult result);
        Task DeleteAsync(PlacementResult result);
        Task SaveChangesAsync();
    }
}