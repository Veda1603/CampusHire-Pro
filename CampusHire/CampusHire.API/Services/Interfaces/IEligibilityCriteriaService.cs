using CampusHire.API.DTOs.Eligibility;

namespace CampusHire.API.Services.Interfaces
{
    public interface IEligibilityCriteriaService
    {
        Task<List<EligibilityCriteriaDto>> GetAllAsync();
        Task<EligibilityCriteriaDto?> GetByIdAsync(int id);
        Task<string> CreateAsync(CreateEligibilityCriteriaDto dto);
        Task<string> UpdateAsync(int id, UpdateEligibilityCriteriaDto dto);
        Task<string> DeleteAsync(int id);
    }
}