using CampusHire.API.Entities;
namespace CampusHire.API.Repositories.Interfaces
{
    public interface IEligibilityCriteriaRepository
    {
        Task<List<EligibilityCriteria>> GetAllAsync();
        Task<EligibilityCriteria?> GetByIdAsync(int id);
        Task AddAsync(EligibilityCriteria criteria);
        Task UpdateAsync(EligibilityCriteria criteria);
        Task DeleteAsync(EligibilityCriteria criteria);
        Task SaveChangesAsync();
    }
}