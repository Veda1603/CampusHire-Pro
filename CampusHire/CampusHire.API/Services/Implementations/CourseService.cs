using AutoMapper;
using CampusHire.API.DTOs.Common;
using CampusHire.API.DTOs.Course;
using CampusHire.API.Entities;
using CampusHire.API.Exceptions;
using CampusHire.API.Helpers;
using CampusHire.API.Repositories.Interfaces;
using CampusHire.API.Services.Interfaces;

namespace CampusHire.API.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;
        private readonly ActivityLogger _logger;
        public CourseService(
            ICourseRepository repository,
            IMapper mapper,
            ActivityLogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<CourseDto>> GetAllAsync()
        {
            var courses = await _repository.GetAllAsync();
            return _mapper.Map<List<CourseDto>>(courses);
        }
        public async Task<CourseDto?> GetByIdAsync(int id)
        {
            var course = await _repository.GetByIdAsync(id);
            if (course == null)
                throw new NotFoundException("Course not found", "COURSE_NOT_FOUND");
            return _mapper.Map<CourseDto>(course);
        }
        public async Task<string> CreateAsync(CreateCourseDto dto)
        {
            var course = _mapper.Map<Course>(dto);
            await _repository.AddAsync(course);
            await _logger.Log(
                1,
                "CREATE_COURSE",
                $"Course {course.CourseName} created"
            );
            await _repository.SaveChangesAsync();
            return "Course created successfully";
        }
        public async Task<string> UpdateAsync(int id, UpdateCourseDto dto)
        {
            var course = await _repository.GetByIdAsync(id);
            if (course == null)
                throw new NotFoundException("Course not found", "COURSE_NOT_FOUND");
            _mapper.Map(dto, course);
            await _repository.UpdateAsync(course);
            await _repository.SaveChangesAsync();
            return "Course updated successfully";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var course = await _repository.GetByIdAsync(id);
            if (course == null)
                throw new NotFoundException("Course not found", "COURSE_NOT_FOUND");
            await _repository.DeleteAsync(course);
            await _repository.SaveChangesAsync();
            return "Course deleted successfully";
        }

        public async Task<IEnumerable<CourseDto>> GetPagedAsync(PaginationDto dto)
        {
            var data = await _repository.GetPagedAsync(dto);
            return _mapper.Map<IEnumerable<CourseDto>>(data);
        }
    }
}