package com.campushire.controller;

import java.util.List;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.CertificationRequest;
import com.campushire.dto.CertificationResponse;
import com.campushire.service.CertificationService;

@RestController
@RequestMapping("/api/certifications")
public class CertificationController {
    private final CertificationService certificationService;
    public CertificationController(CertificationService certificationService) {
        this.certificationService = certificationService;
    }

    @PostMapping
    public CertificationResponse add(@RequestBody CertificationRequest request) {
        return certificationService.addCertification(request);
    }

    @GetMapping("/{studentId}")
    public List<CertificationResponse> get(@PathVariable Integer studentId) {
        return certificationService.getCertifications(studentId);
    }
}