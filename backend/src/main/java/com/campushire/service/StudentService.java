package com.campushire.service;

import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import com.campushire.dto.StudentRequest;
import com.campushire.dto.StudentResponse;
import com.campushire.entity.Student;
import com.campushire.entity.User;
import com.campushire.repository.StudentRepository;
import com.campushire.repository.UserRepository;
import com.campushire.exception.ResourceNotFoundException;

@Service
public class StudentService {

    private final StudentRepository studentRepository;
    private final UserRepository userRepository;
    private final FileStorageService fileStorageService;
    public StudentService(
            StudentRepository studentRepository,
            UserRepository userRepository,
            FileStorageService fileStorageService) {
        this.studentRepository = studentRepository;
        this.userRepository = userRepository;
        this.fileStorageService = fileStorageService;
    }
    public StudentResponse createProfile(StudentRequest request) {
        User user = userRepository.findById(request.getUserId())
                .orElseThrow(() ->
                        new ResourceNotFoundException("User not found"));
        if(studentRepository.findByUserId(user.getId()).isPresent()) {
            throw new RuntimeException("Student profile already exists");
        }
        Student student = Student.builder()
                .user(user)
                .collegeRollNo(request.getCollegeRollNo())
                .prnNumber(request.getPrnNumber())
                .currentYear(request.getCurrentYear())
                .currentCGPA(request.getCurrentCGPA())
                .expectedPassoutYear(request.getExpectedPassoutYear())
                .build();
        Student saved = studentRepository.save(student);
        return mapToResponse(saved);
    }
    public StudentResponse getProfile(String email) {
        User user = userRepository.findByEmail(email)
                .orElseThrow(() ->
                        new ResourceNotFoundException("User not found"));
        Student student = studentRepository.findByUserId(user.getId())
                .orElseThrow(() ->
                        new ResourceNotFoundException("Student profile not found"));
        return mapToResponse(student);
    }
    public StudentResponse updateProfile(
            String email,
            StudentRequest request) {
        User user = userRepository.findByEmail(email)
                .orElseThrow(() ->
                        new ResourceNotFoundException("User not found"));
        Student student = studentRepository.findByUserId(user.getId())
                .orElseThrow(() ->
                        new ResourceNotFoundException("Student profile not found"));
        student.setCollegeRollNo(request.getCollegeRollNo());
        student.setPrnNumber(request.getPrnNumber());
        student.setCurrentYear(request.getCurrentYear());
        student.setCurrentCGPA(request.getCurrentCGPA());
        student.setExpectedPassoutYear(request.getExpectedPassoutYear());
        Student updated = studentRepository.save(student);
        return mapToResponse(updated);
    }
    public StudentResponse uploadResume(
            String email,
            MultipartFile file) {
        User user = userRepository.findByEmail(email)
                .orElseThrow(() ->
                        new ResourceNotFoundException("User not found"));
        Student student = studentRepository.findByUserId(user.getId())
                .orElseThrow(() ->
                        new ResourceNotFoundException("Student profile not found"));
        // Resume upload will move to Resume entity later.
        // Keeping this method temporarily to avoid breaking API.
        String filePath = fileStorageService.saveFile(file);
        Student updated = studentRepository.save(student);
        return mapToResponse(updated);
    }
    private StudentResponse mapToResponse(Student student) {
        User user = student.getUser();
        return StudentResponse.builder()
                .id(student.getId())
                .fullName(user.getFullName())
                .email(user.getEmail())
                .collegeRollNo(student.getCollegeRollNo())
                .prnNumber(student.getPrnNumber())
                .currentYear(student.getCurrentYear())
                .currentCGPA(student.getCurrentCGPA())
                .expectedPassoutYear(student.getExpectedPassoutYear())
                .profileCompleted(student.getProfileCompleted())
                .verificationStatus(
                        student.getVerificationStatus().name()
                )
                .build();
    }
    
}