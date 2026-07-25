package com.campushire.controller;

import java.util.List;

import org.springframework.web.bind.annotation.*;

import com.campushire.dto.EducationRequest;
import com.campushire.dto.EducationResponse;
import com.campushire.service.EducationService;

@RestController
@RequestMapping("/api/education")
public class EducationController {

    private final EducationService educationService;

    public EducationController(EducationService educationService) {
        this.educationService = educationService;
    }

    @PostMapping
    public EducationResponse add(@RequestBody EducationRequest request) {
        return educationService.addEducation(request);
    }

    @GetMapping("/{studentId}")
    public List<EducationResponse> get(@PathVariable Integer studentId) {
        return educationService.getEducation(studentId);
    }
}