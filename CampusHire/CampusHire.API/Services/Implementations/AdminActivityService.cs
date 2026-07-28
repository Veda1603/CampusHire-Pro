using AutoMapper;
using CampusHire.API.DTOs.Admin;
using CampusHire.API.Entities;
using CampusHire.API.Repositories.Interfaces;
using CampusHire.API.Services.Interfaces;

namespace CampusHire.API.Services.Implementations
{
    public class AdminActivityService : IAdminActivityService
    {
        private readonly IAdminActivityRepository _repository;
        private readonly IMapper _mapper;
        public AdminActivityService(
            IAdminActivityRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<AdminActivityDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            return _mapper.Map<List<AdminActivityDto>>(data);
        }
        public async Task<List<AdminActivityDto>> GetByAdminIdAsync(int adminId)
        {
            var data = await _repository.GetByAdminIdAsync(adminId);
            return _mapper.Map<List<AdminActivityDto>>(data);
        }
        public async Task AddAsync(
            int adminId,
            string action,
            string description)
        {
            var activity = new AdminActivityLog
            {
                AdminId = adminId,
                Action = action,
                Description = description,
                CreatedAt = DateTime.UtcNow
            };


            await _repository.AddAsync(activity);
        }
    }
}