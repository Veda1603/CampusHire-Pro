package com.campushire.controller;
import java.util.List;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.interview.InterviewRequestDTO;
import com.campushire.dto.interview.InterviewResponseDTO;
import com.campushire.dto.interview.InterviewStatusUpdateDTO;
import com.campushire.service.InterviewService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
@RestController
@RequestMapping("/api/interviews")
@RequiredArgsConstructor
public class InterviewController {
    private final InterviewService interviewService;
    @PostMapping
    @PreAuthorize("hasRole('RECRUITER')")
    public ResponseEntity<InterviewResponseDTO> createInterview(@Valid @RequestBody InterviewRequestDTO request){
        return ResponseEntity.ok(interviewService.createInterview(request));
    }
    @GetMapping
    @PreAuthorize("hasAnyRole('RECRUITER','STUDENT')")
    public ResponseEntity<List<InterviewResponseDTO>> getAll(){
        return ResponseEntity.ok(interviewService.getAllInterviews());
    }
    @GetMapping("/{id}")
    @PreAuthorize("hasAnyRole('RECRUITER','STUDENT')")
    public ResponseEntity<InterviewResponseDTO> getById(@PathVariable Long id){
        return ResponseEntity.ok(interviewService.getInterviewById(id));
    }
    @GetMapping("/application/{applicationId}")
    @PreAuthorize("hasAnyRole('RECRUITER','STUDENT')")
    public ResponseEntity<List<InterviewResponseDTO>> getByApplication(@PathVariable Long applicationId){
        return ResponseEntity.ok(interviewService.getByApplication(applicationId));
    }
    @PutMapping("/{id}/status")
    @PreAuthorize("hasRole('RECRUITER')")
    public ResponseEntity<InterviewResponseDTO> updateStatus(@PathVariable Long id,@Valid @RequestBody InterviewStatusUpdateDTO request){
        return ResponseEntity.ok(interviewService.updateStatus(id,request.getStatus()));
    }
}