package com.campushire.controller;

import java.util.List;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.JobRequest;
import com.campushire.dto.JobResponse;
import com.campushire.service.JobService;
import jakarta.validation.Valid;

@RestController
@RequestMapping("/api/jobs")
public class JobController {

    private final JobService jobService;

    public JobController(JobService jobService) {
        this.jobService = jobService;
    }

    @PostMapping
    public ResponseEntity<JobResponse> createJob(
            Authentication authentication,
            @Valid @RequestBody JobRequest request) {

        String email = authentication.getName();

        return ResponseEntity.ok(
                jobService.createJob(email, request)
        );
    }

    @GetMapping
    public ResponseEntity<Page<JobResponse>> getAllJobs(
            @RequestParam(defaultValue = "0") int page,
            @RequestParam(defaultValue = "10") int size) {

        Pageable pageable = PageRequest.of(page, size);

        return ResponseEntity.ok(
                jobService.getAllJobs(pageable)
        );
    }

    @GetMapping("/search")
    public ResponseEntity<List<JobResponse>> searchJobs(
            @RequestParam(required = false) String keyword,
            @RequestParam(required = false) String location) {

        if (keyword != null) {
            return ResponseEntity.ok(
                    jobService.searchByKeyword(keyword)
            );
        }

        if (location != null) {
            return ResponseEntity.ok(
                    jobService.searchByLocation(location)
            );
        }

        return ResponseEntity.badRequest().build();
    }

    @GetMapping("/{id}")
    public ResponseEntity<JobResponse> getJob(
            @PathVariable Long id) {

        return ResponseEntity.ok(
                jobService.getJob(id)
        );
    }
}