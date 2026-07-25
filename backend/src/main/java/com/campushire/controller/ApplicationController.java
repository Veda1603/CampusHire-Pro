package com.campushire.controller;

import java.util.List;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.ApplicationRequest;
import com.campushire.dto.ApplicationResponse;
import com.campushire.service.ApplicationService;

@RestController
@RequestMapping("/api/applications")
public class ApplicationController {
    private final ApplicationService applicationService;

    public ApplicationController(ApplicationService applicationService) {
        this.applicationService = applicationService;
    }

    @PreAuthorize("hasRole('STUDENT')")
    @PostMapping("/apply")
    public ResponseEntity<?> applyJob(@RequestBody ApplicationRequest request) {
        return ResponseEntity.ok(applicationService.applyJob(request));
    }

    @PreAuthorize("hasRole('STUDENT')")
    @GetMapping("/student")
    public ResponseEntity<List<ApplicationResponse>> getStudentApplications() {
        return ResponseEntity.ok(applicationService.getStudentApplications());
    }

    @PreAuthorize("hasRole('RECRUITER')")
    @GetMapping("/company")
    public ResponseEntity<List<ApplicationResponse>> getCompanyApplications() {
        return ResponseEntity.ok(applicationService.getCompanyApplications());
    }

    @PreAuthorize("hasRole('RECRUITER')")
    @PutMapping("/{id}/status")
    public ResponseEntity<?> updateStatus(@PathVariable Long id, @RequestParam String status) {
        return ResponseEntity.ok(applicationService.updateApplicationStatus(id, status));
    }
}