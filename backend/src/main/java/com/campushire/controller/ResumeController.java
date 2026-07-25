package com.campushire.controller;

import java.util.List;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.*;
import com.campushire.service.ResumeService;

@RestController
@RequestMapping("/api/resumes")
public class ResumeController {

    private final ResumeService service;

    public ResumeController(ResumeService service){
        this.service=service;
    }

    @PostMapping
    public ResumeResponse add(@RequestBody ResumeRequest request){
        return service.add(request);
    }

    @GetMapping("/{studentId}")
    public List<ResumeResponse> get(@PathVariable Integer studentId){
        return service.get(studentId);
    }
}