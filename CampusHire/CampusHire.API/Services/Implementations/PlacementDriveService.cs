using AutoMapper;
using CampusHire.API.DTOs.PlacementDrive;
using CampusHire.API.Entities;
using CampusHire.API.Exceptions;
using CampusHire.API.Helpers;
using CampusHire.API.Repositories.Interfaces;
using CampusHire.API.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CampusHire.API.Services.Implementations
{
    public class PlacementDriveService : IPlacementDriveService
    {
        private readonly IPlacementDriveRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ActivityLogger _logger;
        public PlacementDriveService(IPlacementDriveRepository repository, IMapper mapper, ActivityLogger logger, IMemoryCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }
        public async Task<List<PlacementDriveDto>> GetAllAsync()
        {
            if (_cache.TryGetValue("placement_drives", out List<PlacementDriveDto>? cachedData))
            {
                return cachedData!;
            }
            var drives = await _repository.GetAllAsync();
            var result = _mapper.Map<List<PlacementDriveDto>>(drives);
            _cache.Set(
                "placement_drives",
                result,
                TimeSpan.FromMinutes(10));
            return result;
        }

        public async Task<PlacementDriveDto?> GetByIdAsync(int id)
        {
            var drive = await _repository.GetByIdAsync(id);
            if (drive == null) return null;
            return _mapper.Map<PlacementDriveDto>(drive);
        }

        public async Task<string> CreateAsync(CreatePlacementDriveDto dto)
        {
            var drive = _mapper.Map<PlacementDrive>(dto);
            await _repository.AddAsync(drive);
            await _logger.Log(1,"CREATE_PLACEMENT_DRIVE",$"Placement drive for {drive.CompanyName} created");
            await _repository.SaveChangesAsync();
            _cache.Remove("placement_drives");
            return "Placement Drive created successfully";
        }

        public async Task<string> UpdateAsync(int id, UpdatePlacementDriveDto dto)
        {
            var drive = await _repository.GetByIdAsync(id);

            if (drive == null)
                throw new NotFoundException("Placement drive not found", "DRIVE_NOT_FOUND");

            _mapper.Map(dto, drive);

            await _repository.UpdateAsync(drive);
            await _repository.SaveChangesAsync();
            _cache.Remove("placement_drives");
            return "Placement Drive updated successfully";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var drive = await _repository.GetByIdAsync(id);

            if (drive == null)
                throw new NotFoundException("Placement drive not found", "DRIVE_NOT_FOUND");

            await _repository.DeleteAsync(drive);
            await _repository.SaveChangesAsync();
            _cache.Remove("placement_drives");
            return "Placement Drive deleted successfully";
        }
    }
}