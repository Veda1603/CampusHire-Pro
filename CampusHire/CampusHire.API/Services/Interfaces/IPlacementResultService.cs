using CampusHire.API.DTOs.PlacementResult;
namespace CampusHire.API.Services.Interfaces
{
    public interface IPlacementResultService
    {
        Task<List<PlacementResultDto>> GetAllAsync();
        Task<PlacementResultDto?> GetByIdAsync(int id);
        Task<string> CreateAsync(CreatePlacementResultDto dto);
        Task<string> UpdateAsync(int id, UpdatePlacementResultDto dto);
        Task<string> DeleteAsync(int id);
    }
}