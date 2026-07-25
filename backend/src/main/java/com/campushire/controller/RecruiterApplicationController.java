package com.campushire.controller;

import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import com.campushire.dto.ApplicationStatsResponse;
import com.campushire.service.ApplicationService;

@RestController
@RequestMapping("/api/recruiter")
public class RecruiterApplicationController {

    private final ApplicationService applicationService;

    public RecruiterApplicationController(ApplicationService applicationService) {
        this.applicationService = applicationService;
    }

    @PreAuthorize("hasRole('RECRUITER')")
    @GetMapping("/applications/stats")
    public ResponseEntity<ApplicationStatsResponse> getApplicationStats() {
        return ResponseEntity.ok(
                applicationService.getApplicationStats()
        );
    }
}