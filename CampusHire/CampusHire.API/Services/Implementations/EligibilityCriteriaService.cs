using AutoMapper;
using CampusHire.API.DTOs.Eligibility;
using CampusHire.API.Entities;
using CampusHire.API.Repositories.Interfaces;
using CampusHire.API.Services.Interfaces;

namespace CampusHire.API.Services.Implementations
{
    public class EligibilityCriteriaService : IEligibilityCriteriaService
    {
        private readonly IEligibilityCriteriaRepository _repository;
        private readonly IMapper _mapper;
        public EligibilityCriteriaService(IEligibilityCriteriaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<EligibilityCriteriaDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            return _mapper.Map<List<EligibilityCriteriaDto>>(data);
        }
        public async Task<EligibilityCriteriaDto?> GetByIdAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
                return null;
            return _mapper.Map<EligibilityCriteriaDto>(data);
        }
        public async Task<string> CreateAsync(CreateEligibilityCriteriaDto dto)
        {
            var entity = _mapper.Map<EligibilityCriteria>(dto);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return "Eligibility criteria created successfully";
        }
        public async Task<string> UpdateAsync(int id, UpdateEligibilityCriteriaDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return "Eligibility criteria not found";
            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
            return "Eligibility criteria updated successfully";
        }
        public async Task<string> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return "Eligibility criteria not found";
            await _repository.DeleteAsync(entity);
            await _repository.SaveChangesAsync();
            return "Eligibility criteria deleted successfully";
        }
    }
}