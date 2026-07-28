using CampusHire.API.Entities;

namespace CampusHire.API.Repositories.Interfaces
{
    public interface IPlacementDriveRepository
    {
        Task<List<PlacementDrive>> GetAllAsync();
        Task<PlacementDrive?> GetByIdAsync(int id);
        Task AddAsync(PlacementDrive drive);
        Task UpdateAsync(PlacementDrive drive);
        Task DeleteAsync(PlacementDrive drive);
        Task SaveChangesAsync();
    }
}