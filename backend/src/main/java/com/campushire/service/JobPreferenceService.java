package com.campushire.service;
import org.springframework.stereotype.Service;
import com.campushire.dto.*;
import com.campushire.entity.*;
import com.campushire.repository.*;

@Service
public class JobPreferenceService{

    private final JobPreferenceRepository repository;
    private final StudentRepository studentRepository;

    public JobPreferenceService(JobPreferenceRepository repository,StudentRepository studentRepository){
        this.repository=repository;
        this.studentRepository=studentRepository;
    }

    public JobPreferenceResponse save(JobPreferenceRequest request){
        Student student=studentRepository.findById(request.getStudentId()).orElseThrow();
        JobPreference preference=JobPreference.builder()
                .student(student)
                .preferredJobType(request.getPreferredJobType())
                .preferredLocation(request.getPreferredLocation())
                .preferredIndustry(request.getPreferredIndustry())
                .expectedSalary(request.getExpectedSalary())
                .openToRelocation(request.getOpenToRelocation())
                .build();
        return map(repository.save(preference));
    }

    public JobPreferenceResponse get(Integer studentId){
        return map(repository.findByStudentId(studentId));
    }

    private JobPreferenceResponse map(JobPreference p){
        return JobPreferenceResponse.builder()
                .id(p.getId())
                .preferredJobType(p.getPreferredJobType())
                .preferredLocation(p.getPreferredLocation())
                .preferredIndustry(p.getPreferredIndustry())
                .expectedSalary(p.getExpectedSalary())
                .openToRelocation(p.getOpenToRelocation())
                .build();
    }
}