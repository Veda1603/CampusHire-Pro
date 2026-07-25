package com.campushire.controller;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.*;
import com.campushire.service.JobPreferenceService;

@RestController
@RequestMapping("/api/job-preferences")
public class JobPreferenceController{

    private final JobPreferenceService service;

    public JobPreferenceController(JobPreferenceService service){
        this.service=service;
    }

    @PostMapping
    public JobPreferenceResponse save(@RequestBody JobPreferenceRequest request){
        return service.save(request);
    }

    @GetMapping("/{studentId}")
    public JobPreferenceResponse get(@PathVariable Integer studentId){
        return service.get(studentId);
    }
}