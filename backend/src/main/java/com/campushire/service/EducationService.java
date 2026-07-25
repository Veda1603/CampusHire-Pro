package com.campushire.service;

import java.util.List;

import org.springframework.stereotype.Service;

import com.campushire.dto.EducationRequest;
import com.campushire.dto.EducationResponse;
import com.campushire.entity.Education;
import com.campushire.entity.Student;
import com.campushire.repository.EducationRepository;
import com.campushire.repository.StudentRepository;
import com.campushire.exception.ResourceNotFoundException;

@Service
public class EducationService {

    private final EducationRepository educationRepository;
    private final StudentRepository studentRepository;

    public EducationService(EducationRepository educationRepository, StudentRepository studentRepository) {
        this.educationRepository = educationRepository;
        this.studentRepository = studentRepository;
    }

    public EducationResponse addEducation(EducationRequest request) {
        Student student = studentRepository.findById(request.getStudentId())
                .orElseThrow(() -> new ResourceNotFoundException("Student not found"));

        Education education = Education.builder()
                .student(student)
                .qualification(request.getQualification())
                .collegeName(request.getCollegeName())
                .university(request.getUniversity())
                .course(request.getCourse())
                .stream(request.getStream())
                .gradingSystem(request.getGradingSystem())
                .score(request.getScore())
                .passoutYear(request.getPassoutYear())
                .marksheetUrl(request.getMarksheetUrl())
                .build();

        return mapToResponse(educationRepository.save(education));
    }

    public List<EducationResponse> getEducation(Integer studentId) {
        return educationRepository.findByStudentId(studentId)
                .stream()
                .map(this::mapToResponse)
                .toList();
    }

    private EducationResponse mapToResponse(Education education) {
        return EducationResponse.builder()
                .id(education.getId())
                .qualification(education.getQualification())
                .collegeName(education.getCollegeName())
                .university(education.getUniversity())
                .course(education.getCourse())
                .stream(education.getStream())
                .gradingSystem(education.getGradingSystem())
                .score(education.getScore())
                .passoutYear(education.getPassoutYear())
                .marksheetUrl(education.getMarksheetUrl())
                .build();
    }
}