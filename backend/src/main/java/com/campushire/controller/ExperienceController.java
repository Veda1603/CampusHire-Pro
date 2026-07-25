package com.campushire.controller;

import java.util.List;
import org.springframework.web.bind.annotation.*;

import com.campushire.dto.ExperienceRequest;
import com.campushire.dto.ExperienceResponse;
import com.campushire.service.ExperienceService;

@RestController
@RequestMapping("/api/experience")
public class ExperienceController {
    private final ExperienceService experienceService;
    public ExperienceController(ExperienceService experienceService) {
        this.experienceService = experienceService;
    }
    @PostMapping
    public ExperienceResponse add(@RequestBody ExperienceRequest request) {
        return experienceService.addExperience(request);
    }
    @GetMapping("/{studentId}")
    public List<ExperienceResponse> get(@PathVariable Integer studentId) {
        return experienceService.getExperiences(studentId);
    }
}