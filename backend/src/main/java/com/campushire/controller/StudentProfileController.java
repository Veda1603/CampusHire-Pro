package com.campushire.controller;

import org.springframework.web.bind.annotation.*;

import com.campushire.dto.StudentProfileRequest;
import com.campushire.dto.StudentProfileResponse;
import com.campushire.service.StudentProfileService;

@RestController
@RequestMapping("/api/student-profile")
public class StudentProfileController {
    private final StudentProfileService profileService;
    public StudentProfileController(StudentProfileService profileService) {
        this.profileService = profileService;
    }
    @PostMapping
    public StudentProfileResponse create(
            @RequestBody StudentProfileRequest request) {
        return profileService.createProfile(request);
    }
    @GetMapping("/{studentId}")
    public StudentProfileResponse get(
            @PathVariable Integer studentId) {
        return profileService.getProfile(studentId);
    }
}