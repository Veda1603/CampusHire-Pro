package com.campushire.controller;

import java.util.List;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.ProjectRequest;
import com.campushire.dto.ProjectResponse;
import com.campushire.service.ProjectService;

@RestController
@RequestMapping("/api/projects")
public class ProjectController {
    private final ProjectService projectService;
    public ProjectController(ProjectService projectService) {
        this.projectService = projectService;
    }

    @PostMapping
    public ProjectResponse add(@RequestBody ProjectRequest request) {
        return projectService.addProject(request);
    }

    @GetMapping("/{studentId}")
    public List<ProjectResponse> get(@PathVariable Integer studentId) {
        return projectService.getProjects(studentId);
    }
}