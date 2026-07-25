package com.campushire.controller;

import java.util.List;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.LanguageRequest;
import com.campushire.dto.LanguageResponse;
import com.campushire.service.LanguageService;

@RestController
@RequestMapping("/api/languages")
public class LanguageController {
    private final LanguageService languageService;
    public LanguageController(LanguageService languageService) {
        this.languageService = languageService;
    }

    @PostMapping
    public LanguageResponse add(@RequestBody LanguageRequest request) {
        return languageService.addLanguage(request);
    }

    @GetMapping("/{studentId}")
    public List<LanguageResponse> get(@PathVariable Integer studentId) {
        return languageService.getLanguages(studentId);
    }
}