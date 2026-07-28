using AutoMapper;
using CampusHire.API.DTOs.RegistrationDeadline;
using CampusHire.API.Entities;
using CampusHire.API.Repositories.Interfaces;
using CampusHire.API.Services.Interfaces;

namespace CampusHire.API.Services.Implementations
{
    public class RegistrationDeadlineService : IRegistrationDeadlineService
    {
        private readonly IRegistrationDeadlineRepository _repository;
        private readonly IMapper _mapper;

        public RegistrationDeadlineService(
            IRegistrationDeadlineRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<RegistrationDeadlineDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            return _mapper.Map<List<RegistrationDeadlineDto>>(data);
        }

        public async Task<RegistrationDeadlineDto?> GetByIdAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);

            if (data == null)
                return null;

            return _mapper.Map<RegistrationDeadlineDto>(data);
        }

        public async Task<string> CreateAsync(CreateRegistrationDeadlineDto dto)
        {
            var entity = _mapper.Map<RegistrationDeadline>(dto);

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return "Registration deadline created successfully";
        }

        public async Task<string> UpdateAsync(int id, UpdateRegistrationDeadlineDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return "Registration deadline not found";

            _mapper.Map(dto, entity);

            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();

            return "Registration deadline updated successfully";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return "Registration deadline not found";

            await _repository.DeleteAsync(entity);
            await _repository.SaveChangesAsync();

            return "Registration deadline deleted successfully";
        }
    }
}