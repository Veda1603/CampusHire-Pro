using CampusHire.API.DTOs.PlacementDrive;

namespace CampusHire.API.Services.Interfaces
{
    public interface IPlacementDriveService
    {
        Task<List<PlacementDriveDto>> GetAllAsync();
        Task<PlacementDriveDto?> GetByIdAsync(int id);
        Task<string> CreateAsync(CreatePlacementDriveDto dto);
        Task<string> UpdateAsync(int id, UpdatePlacementDriveDto dto);
        Task<string> DeleteAsync(int id);
    }
}