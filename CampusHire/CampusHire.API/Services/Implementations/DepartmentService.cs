using AutoMapper;
using CampusHire.API.DTOs.Common;
using CampusHire.API.DTOs.Department;
using CampusHire.API.Entities;
using CampusHire.API.Exceptions;
using CampusHire.API.Helpers;
using CampusHire.API.Repositories.Interfaces;
using CampusHire.API.Services.Interfaces;

namespace CampusHire.API.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly ActivityLogger _logger;

        public DepartmentService(IDepartmentRepository repository, IMapper mapper, ActivityLogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            var departments = await _repository.GetAllAsync();
            return _mapper.Map<List<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);
            if (department == null)
                throw new NotFoundException("Department not found", "DEPARTMENT_NOT_FOUND");
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<string> CreateAsync(CreateDepartmentDto dto)
        {
            if (await _repository.ExistsByCodeAsync(dto.DepartmentCode))
                throw new BadRequestException("Department code already exists", "DEPARTMENT_CODE_EXISTS");

            var department = _mapper.Map<Department>(dto);
            department.IsActive = true;

            await _repository.AddAsync(department);
            await _logger.Log(1, "CREATE_DEPARTMENT", $"Department {department.DepartmentName} created");
            await _repository.SaveChangesAsync();

            return "Department created successfully";
        }

        public async Task<string> UpdateAsync(int id, UpdateDepartmentDto dto)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
                throw new NotFoundException("Department not found", "DEPARTMENT_NOT_FOUND");

            _mapper.Map(dto, department);
            await _repository.UpdateAsync(department);
            await _repository.SaveChangesAsync();

            return "Department updated successfully";
        }

        public async Task<IEnumerable<DepartmentDto>> GetPagedAsync(PaginationDto dto)
        {
            var data = await _repository.GetPagedAsync(dto);
            return _mapper.Map<IEnumerable<DepartmentDto>>(data);
        }

        public async Task<string> DeleteAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
                throw new NotFoundException("Department not found", "DEPARTMENT_NOT_FOUND");

            await _repository.DeleteAsync(department);
            await _repository.SaveChangesAsync();

            return "Department deleted successfully";
        }
    }
}