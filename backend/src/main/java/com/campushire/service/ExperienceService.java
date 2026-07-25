package com.campushire.service;

import java.util.List;

import org.springframework.stereotype.Service;

import com.campushire.dto.ExperienceRequest;
import com.campushire.dto.ExperienceResponse;
import com.campushire.entity.Experience;
import com.campushire.entity.Student;
import com.campushire.exception.ResourceNotFoundException;
import com.campushire.repository.ExperienceRepository;
import com.campushire.repository.StudentRepository;

@Service
public class ExperienceService {
    private final ExperienceRepository experienceRepository;
    private final StudentRepository studentRepository;
    public ExperienceService(ExperienceRepository experienceRepository, StudentRepository studentRepository) {
        this.experienceRepository = experienceRepository;
        this.studentRepository = studentRepository;
    }
    public ExperienceResponse addExperience(ExperienceRequest request) {
        Student student = studentRepository.findById(request.getStudentId())
                .orElseThrow(() -> new ResourceNotFoundException("Student not found"));
        Experience experience = Experience.builder()
                .student(student)
                .experienceType(request.getExperienceType())
                .companyName(request.getCompanyName())
                .jobTitle(request.getJobTitle())
                .startDate(request.getStartDate())
                .endDate(request.getEndDate())
                .currentlyWorking(request.getCurrentlyWorking())
                .experienceLetterUrl(request.getExperienceLetterUrl())
                .build();
        return mapToResponse(experienceRepository.save(experience));
    }
    public List<ExperienceResponse> getExperiences(Integer studentId) {
        return experienceRepository.findByStudentId(studentId)
                .stream()
                .map(this::mapToResponse)
                .toList();
    }
    private ExperienceResponse mapToResponse(Experience experience) {
        return ExperienceResponse.builder()
                .id(experience.getId())
                .experienceType(experience.getExperienceType())
                .companyName(experience.getCompanyName())
                .jobTitle(experience.getJobTitle())
                .startDate(experience.getStartDate())
                .endDate(experience.getEndDate())
                .currentlyWorking(experience.getCurrentlyWorking())
                .experienceLetterUrl(experience.getExperienceLetterUrl())
                .build();
    }
}