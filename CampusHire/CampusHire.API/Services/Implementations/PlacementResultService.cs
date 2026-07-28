using AutoMapper;
using CampusHire.API.DTOs.PlacementResult;
using CampusHire.API.Entities;
using CampusHire.API.Repositories.Interfaces;
using CampusHire.API.Services.Interfaces;
namespace CampusHire.API.Services.Implementations
{
    public class PlacementResultService : IPlacementResultService
    {
        private readonly IPlacementResultRepository _repository;
        private readonly IMapper _mapper;
        public PlacementResultService(IPlacementResultRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<PlacementResultDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            return _mapper.Map<List<PlacementResultDto>>(data);
        }
        public async Task<PlacementResultDto?> GetByIdAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
                return null;
            return _mapper.Map<PlacementResultDto>(data);
        }
        public async Task<string> CreateAsync(CreatePlacementResultDto dto)
        {
            var result = _mapper.Map<PlacementResult>(dto);
            await _repository.AddAsync(result);
            await _repository.SaveChangesAsync();
            return "Placement result created successfully";
        }
        public async Task<string> UpdateAsync(int id, UpdatePlacementResultDto dto)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return "Placement result not found";
            _mapper.Map(dto, result);
            await _repository.UpdateAsync(result);
            await _repository.SaveChangesAsync();
            return "Placement result updated successfully";
        }
        public async Task<string> DeleteAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return "Placement result not found";
            await _repository.DeleteAsync(result);
            await _repository.SaveChangesAsync();
            return "Placement result deleted successfully";
        }
    }
}