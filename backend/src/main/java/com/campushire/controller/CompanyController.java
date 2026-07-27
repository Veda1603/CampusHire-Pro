package com.campushire.controller;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.CompanyRequest;
import com.campushire.dto.CompanyResponse;
import com.campushire.service.CompanyService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;

@RestController
@RequestMapping("/api/company")
@RequiredArgsConstructor
public class CompanyController {
    private final CompanyService companyService;
    @PostMapping
    @PreAuthorize("hasRole('RECRUITER')")
    public ResponseEntity<CompanyResponse> createCompany(@Valid @RequestBody CompanyRequest request){
        return ResponseEntity.ok(companyService.createCompany(request));
    }
    @GetMapping("/{id}")
    @PreAuthorize("hasAnyRole('RECRUITER','STUDENT')")
    public ResponseEntity<CompanyResponse> getCompany(@PathVariable Long id){
        return ResponseEntity.ok(companyService.getCompany(id));
    }
    @PutMapping("/{id}")
    @PreAuthorize("hasRole('RECRUITER')")
    public ResponseEntity<CompanyResponse> updateCompany(@PathVariable Long id,@Valid @RequestBody CompanyRequest request){
        return ResponseEntity.ok(companyService.updateCompany(id,request));
    }
}