package com.campushire.service;

import org.springframework.stereotype.Service;

import com.campushire.dto.RecruiterRequest;
import com.campushire.dto.RecruiterResponse;
import com.campushire.entity.Company;
import com.campushire.entity.Recruiter;
import com.campushire.entity.User;
import com.campushire.repository.CompanyRepository;
import com.campushire.repository.RecruiterRepository;
import com.campushire.repository.UserRepository;

@Service
public class RecruiterService {

    private final RecruiterRepository recruiterRepository;
    private final CompanyRepository companyRepository;
    private final UserRepository userRepository;

    public RecruiterService(
            RecruiterRepository recruiterRepository,
            CompanyRepository companyRepository,
            UserRepository userRepository) {

        this.recruiterRepository = recruiterRepository;
        this.companyRepository = companyRepository;
        this.userRepository = userRepository;
    }

    // CREATE RECRUITER PROFILE
    public RecruiterResponse createProfile(
            String email,
            RecruiterRequest request) {

        User user = userRepository.findByEmail(email)
                .orElseThrow(() ->
                new RuntimeException("User not found"));

        Company company = companyRepository.findById(request.getCompanyId())
                .orElseThrow(() ->
                new RuntimeException("Company not found"));

        Recruiter recruiter = new Recruiter();

        recruiter.setUser(user);
        recruiter.setCompany(company);
        recruiter.setDesignation(request.getDesignation());
        recruiter.setPhoneNumber(request.getPhoneNumber());

        Recruiter saved = recruiterRepository.save(recruiter);

        return mapToResponse(saved);
    }

    // GET PROFILE
    public RecruiterResponse getProfile(String email) {

        User user = userRepository.findByEmail(email)
                .orElseThrow(() ->
                new RuntimeException("User not found"));

        Recruiter recruiter = recruiterRepository
                .findByUserId(user.getId())
                .orElseThrow(() ->
                new RuntimeException("Recruiter profile not found"));

        return mapToResponse(recruiter);
    }

    // UPDATE PROFILE
    public RecruiterResponse updateProfile(
            String email,
            RecruiterRequest request) {

        User user = userRepository.findByEmail(email)
                .orElseThrow(() ->
                new RuntimeException("User not found"));

        Recruiter recruiter = recruiterRepository
                .findByUserId(user.getId())
                .orElseThrow(() ->
                new RuntimeException("Recruiter profile not found"));

        Company company = companyRepository.findById(request.getCompanyId())
                .orElseThrow(() ->
                new RuntimeException("Company not found"));

        recruiter.setCompany(company);
        recruiter.setDesignation(request.getDesignation());
        recruiter.setPhoneNumber(request.getPhoneNumber());

        Recruiter updated = recruiterRepository.save(recruiter);

        return mapToResponse(updated);
    }

    private RecruiterResponse mapToResponse(
            Recruiter recruiter) {

        RecruiterResponse response = new RecruiterResponse();

        response.setId(recruiter.getId());

        response.setUserId(
                recruiter.getUser().getId()
        );

        response.setEmail(
                recruiter.getUser().getEmail()
        );

        response.setCompanyName(
                recruiter.getCompany().getCompanyName()
        );

        response.setDesignation(
                recruiter.getDesignation()
        );

        response.setPhoneNumber(
                recruiter.getPhoneNumber()
        );

        return response;
    }
}