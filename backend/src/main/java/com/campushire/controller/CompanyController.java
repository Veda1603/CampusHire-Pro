package com.campushire.controller;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import com.campushire.dto.CompanyRequest;
import com.campushire.dto.CompanyResponse;
import com.campushire.service.CompanyService;

@RestController
@RequestMapping("/api/company")
public class CompanyController {
    private final CompanyService companyService;
    public CompanyController(CompanyService companyService) {
        this.companyService = companyService;
    }
    // CREATE COMPANY
    @PostMapping
    public ResponseEntity<CompanyResponse> createCompany(
            @RequestBody CompanyRequest request) {
        return ResponseEntity.ok(
                companyService.createCompany(request)
        );
    }
    // GET COMPANY
    @GetMapping("/{id}")
    public ResponseEntity<CompanyResponse> getCompany(
            @PathVariable Long id) {
        return ResponseEntity.ok(
                companyService.getCompany(id)
        );
    }
    // UPDATE COMPANY
    @PutMapping("/{id}")
    public ResponseEntity<CompanyResponse> updateCompany(
            @PathVariable Long id,
            @RequestBody CompanyRequest request) {
        return ResponseEntity.ok(
                companyService.updateCompany(id, request)
        );
    }
}