package com.campushire.service;

import org.springframework.stereotype.Service;

import com.campushire.dto.StudentProfileRequest;
import com.campushire.dto.StudentProfileResponse;
import com.campushire.entity.Student;
import com.campushire.entity.StudentProfile;
import com.campushire.repository.StudentProfileRepository;
import com.campushire.repository.StudentRepository;
import com.campushire.exception.ResourceNotFoundException;

@Service
public class StudentProfileService {
    private final StudentProfileRepository profileRepository;
    private final StudentRepository studentRepository;
    public StudentProfileService(
            StudentProfileRepository profileRepository,
            StudentRepository studentRepository) {
        this.profileRepository = profileRepository;
        this.studentRepository = studentRepository;
    }
    public StudentProfileResponse createProfile(StudentProfileRequest request) {
        Student student = studentRepository.findById(request.getStudentId())
                .orElseThrow(() ->
                        new ResourceNotFoundException("Student not found"));
        StudentProfile profile = StudentProfile.builder()
                .student(student)
                .firstName(request.getFirstName())
                .lastName(request.getLastName())
                .phoneNumber(request.getPhoneNumber())
                .dateOfBirth(request.getDateOfBirth())
                .gender(request.getGender())
                .nationality(request.getNationality())
                .maritalStatus(request.getMaritalStatus())
                .addressLine1(request.getAddressLine1())
                .addressLine2(request.getAddressLine2())
                .city(request.getCity())
                .taluka(request.getTaluka())
                .district(request.getDistrict())
                .state(request.getState())
                .country(request.getCountry())
                .pincode(request.getPincode())
                .profilePhoto(request.getProfilePhoto())
                .build();
        StudentProfile saved = profileRepository.save(profile);
        return mapToResponse(saved);
    }
    public StudentProfileResponse getProfile(Integer studentId) {
        StudentProfile profile = profileRepository.findByStudentId(studentId)
                .orElseThrow(() ->
                        new ResourceNotFoundException("Profile not found"));
        return mapToResponse(profile);
    }
    private StudentProfileResponse mapToResponse(StudentProfile profile) {
        return StudentProfileResponse.builder()
                .id(profile.getId())
                .firstName(profile.getFirstName())
                .lastName(profile.getLastName())
                .phoneNumber(profile.getPhoneNumber())
                .dateOfBirth(profile.getDateOfBirth())
                .gender(profile.getGender())
                .nationality(profile.getNationality())
                .maritalStatus(profile.getMaritalStatus())
                .addressLine1(profile.getAddressLine1())
                .addressLine2(profile.getAddressLine2())
                .city(profile.getCity())
                .taluka(profile.getTaluka())
                .district(profile.getDistrict())
                .state(profile.getState())
                .country(profile.getCountry())
                .pincode(profile.getPincode())
                .profilePhoto(profile.getProfilePhoto())
                .build();
    }
}