package com.campushire.controller;

import java.util.List;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import com.campushire.dto.JobResponse;
import com.campushire.service.JobService;

@RestController
@RequestMapping("/api/recruiter")
public class RecruiterJobController {
    private final JobService jobService;

    public RecruiterJobController(JobService jobService) {
        this.jobService = jobService;
    }

    @PreAuthorize("hasRole('RECRUITER')")
    @GetMapping("/jobs")
    public ResponseEntity<List<JobResponse>> getRecruiterJobs() {
        return ResponseEntity.ok(
                jobService.getRecruiterJobs()
        );
    }
}