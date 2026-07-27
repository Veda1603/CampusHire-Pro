package com.campushire.controller;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.RecruiterDashboardResponse;
import com.campushire.service.RecruiterDashboardService;
import lombok.RequiredArgsConstructor;

@RestController
@RequestMapping("/api/recruiter")
@RequiredArgsConstructor
public class RecruiterDashboardController {
    private final RecruiterDashboardService recruiterDashboardService;

    @PreAuthorize("hasRole('RECRUITER')")
    @GetMapping("/dashboard")
    public ResponseEntity<RecruiterDashboardResponse> getDashboard(){
        return ResponseEntity.ok(recruiterDashboardService.getDashboard());
    }
}