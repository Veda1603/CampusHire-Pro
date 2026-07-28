using AutoMapper;
using CampusHire.API.DTOs.Admin;
using CampusHire.API.DTOs.Course;
using CampusHire.API.DTOs.Department;
using CampusHire.API.DTOs.Eligibility;
using CampusHire.API.DTOs.PlacementDrive;
using CampusHire.API.DTOs.PlacementResult;
using CampusHire.API.DTOs.RegistrationDeadline;
using CampusHire.API.DTOs.Student;
using CampusHire.API.Entities;

namespace CampusHire.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();
            CreateMap<Course, CourseDto>()
            .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Department.DepartmentName));
            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();
            CreateMap<PlacementDrive, PlacementDriveDto>();
            CreateMap<CreatePlacementDriveDto, PlacementDrive>();
            CreateMap<UpdatePlacementDriveDto, PlacementDrive>();
            CreateMap<StudentVerification, StudentVerificationDto>();
            CreateMap<CreateStudentVerificationDto, StudentVerification>();
            CreateMap<UpdateStudentVerificationDto, StudentVerification>();
            CreateMap<Admin, AdminDto>().ReverseMap();
            CreateMap<Admin, AdminProfileDto>().ReverseMap();
            CreateMap<AdminRegisterDto, Admin>();
            CreateMap<UpdateAdminDto, Admin>();
            CreateMap<AdminActivityLog, AdminActivityDto>();
            CreateMap<CreateStudentVerificationDto, StudentVerification>();
            CreateMap<UpdateStudentVerificationDto, StudentVerification>();
            CreateMap<StudentVerification, StudentVerificationDto>();
            CreateMap<EligibilityCriteria, EligibilityCriteriaDto>();
            CreateMap<CreateEligibilityCriteriaDto, EligibilityCriteria>();
            CreateMap<UpdateEligibilityCriteriaDto, EligibilityCriteria>();
            CreateMap<RegistrationDeadline, RegistrationDeadlineDto>();
            CreateMap<CreateRegistrationDeadlineDto, RegistrationDeadline>();
            CreateMap<UpdateRegistrationDeadlineDto, RegistrationDeadline>();
            CreateMap<PlacementResult, PlacementResultDto>();
            CreateMap<CreatePlacementResultDto, PlacementResult>();
            CreateMap<UpdatePlacementResultDto, PlacementResult>();
        }
    }
}