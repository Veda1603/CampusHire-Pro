package com.campushire.controller;

import java.util.List;

import org.springframework.web.bind.annotation.*;

import com.campushire.dto.SkillRequest;
import com.campushire.dto.SkillResponse;
import com.campushire.service.SkillService;

@RestController
@RequestMapping("/api/skills")
public class SkillController {
    private final SkillService skillService;
    public SkillController(SkillService skillService) {
        this.skillService = skillService;
    }
    @PostMapping
    public SkillResponse add(@RequestBody SkillRequest request) {
        return skillService.addSkill(request);
    }
    @GetMapping("/{studentId}")
    public List<SkillResponse> get(@PathVariable Integer studentId) {
        return skillService.getSkills(studentId);
    }
}