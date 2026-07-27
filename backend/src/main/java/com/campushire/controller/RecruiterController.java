package com.campushire.controller;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.ApplicantResponse;
import com.campushire.dto.RecruiterRequest;
import com.campushire.dto.RecruiterResponse;
import com.campushire.service.RecruiterService;
import jakarta.validation.Valid;

@RestController
@RequestMapping("/api/recruiter")
public class RecruiterController {
    private final RecruiterService recruiterService;

    public RecruiterController(RecruiterService recruiterService){
        this.recruiterService=recruiterService;
    }

    @PreAuthorize("hasRole('RECRUITER')")
    @GetMapping("/applicants/{applicationId}")
    public ResponseEntity<ApplicantResponse> getApplicant(Authentication authentication,@PathVariable Long applicationId){
        String email=authentication.getName();
        return ResponseEntity.ok(recruiterService.getApplicant(email,applicationId));
    }

    @PreAuthorize("hasRole('RECRUITER')")
    @PostMapping("/profile")
    public ResponseEntity<RecruiterResponse> createProfile(Authentication authentication,@Valid @RequestBody RecruiterRequest request){
        String email=authentication.getName();
        return ResponseEntity.ok(recruiterService.createProfile(email,request));
    }

    @PreAuthorize("hasRole('RECRUITER')")
    @GetMapping("/profile")
    public ResponseEntity<RecruiterResponse> getProfile(Authentication authentication){
        String email=authentication.getName();
        return ResponseEntity.ok(recruiterService.getProfile(email));
    }

    @PreAuthorize("hasRole('RECRUITER')")
    @PutMapping("/profile")
    public ResponseEntity<RecruiterResponse> updateProfile(Authentication authentication,@Valid @RequestBody RecruiterRequest request){
        String email=authentication.getName();
        return ResponseEntity.ok(recruiterService.updateProfile(email,request));
    }
}