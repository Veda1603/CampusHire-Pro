package com.campushire.controller;

import org.springframework.http.ResponseEntity;
import org.springframework.web.multipart.MultipartFile;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.*;

import com.campushire.dto.StudentRequest;
import com.campushire.dto.StudentResponse;
import com.campushire.service.StudentService;

import jakarta.validation.Valid;

@RestController
@RequestMapping("/api/student")
public class StudentController {
    private final StudentService studentService;
    public StudentController(StudentService studentService) {
        this.studentService = studentService;
    }

    // CREATE PROFILE
    @PostMapping("/profile")
    public ResponseEntity<StudentResponse> createProfile(
            @Valid @RequestBody StudentRequest request) {
        System.out.println("POST API HIT");
        return ResponseEntity.ok(
                studentService.createProfile(request)
        );
    }


    // GET PROFILE
    @GetMapping("/profile")
    public ResponseEntity<StudentResponse> getProfile(
            Authentication authentication) {
        System.out.println("GET API HIT");
        String email = authentication.getName();
        return ResponseEntity.ok(
                studentService.getProfile(email)
        );
    }


    // UPDATE PROFILE
    @PutMapping("/profile")
    public ResponseEntity<StudentResponse> updateProfile(
            Authentication authentication,
            @Valid @RequestBody StudentRequest request) {
        System.out.println("PUT API HIT");
        String email = authentication.getName();
        return ResponseEntity.ok(
                studentService.updateProfile(email, request)
        );
    }
    
    @PostMapping("/resume/upload")
    public ResponseEntity<StudentResponse> uploadResume(
            Authentication authentication,
            @RequestParam("file") MultipartFile file) {
        String email = authentication.getName();
        return ResponseEntity.ok(
                studentService.uploadResume(email, file)
        );
    }
}