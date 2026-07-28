using AutoMapper;
using CampusHire.API.DTOs.Student;
using CampusHire.API.Entities;
using CampusHire.API.Helpers;
using CampusHire.API.Repositories.Interfaces;
using CampusHire.API.Services.Interfaces;

namespace CampusHire.API.Services.Implementations
{
    public class StudentVerificationService : IStudentVerificationService
    {
        private readonly IStudentVerificationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ActivityLogger _logger;
        public StudentVerificationService(IStudentVerificationRepository repository,IMapper mapper,ActivityLogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<StudentVerificationDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            return _mapper.Map<List<StudentVerificationDto>>(data);
        }

        public async Task<StudentVerificationDto?> GetByIdAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);

            if (data == null)
                return null;

            return _mapper.Map<StudentVerificationDto>(data);
        }

        public async Task<string> CreateAsync(CreateStudentVerificationDto dto)
        {
            var verification = _mapper.Map<StudentVerification>(dto);
            verification.VerifiedByAdminId = dto.VerifiedByAdminId;
            verification.VerificationType = dto.VerificationType;
            verification.VerifiedOn = DateTime.UtcNow;
            await _repository.AddAsync(verification);
            await _logger.Log(1,"VERIFY_STUDENT",$"Student {verification.StudentId} verification completed");
            await _repository.SaveChangesAsync();

            return "Student verification created successfully";
        }

        public async Task<string> UpdateAsync(int id, UpdateStudentVerificationDto dto)
        {
            var verification = await _repository.GetByIdAsync(id);

            if (verification == null)
                return "Student verification not found";

            verification.Status = dto.Status;
            verification.Remarks = dto.Remarks;
            verification.VerifiedByAdminId = dto.VerifiedByAdminId;
            verification.VerificationType = dto.VerificationType;
            verification.VerifiedOn = DateTime.UtcNow;

            await _repository.UpdateAsync(verification);
            await _repository.SaveChangesAsync();

            return "Student verification updated successfully";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var verification = await _repository.GetByIdAsync(id);

            if (verification == null)
                return "Student verification not found";

            await _repository.DeleteAsync(verification);
            await _repository.SaveChangesAsync();

            return "Student verification deleted successfully";
        }
    }
}