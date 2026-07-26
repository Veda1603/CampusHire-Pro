package com.campushire.service;

import org.springframework.stereotype.Service;

import com.campushire.dto.ApplicantResponse;
import com.campushire.dto.RecruiterRequest;
import com.campushire.dto.RecruiterResponse;
import com.campushire.entity.Application;
import com.campushire.entity.Company;
import com.campushire.entity.Recruiter;
import com.campushire.entity.Resume;
import com.campushire.entity.User;
import com.campushire.repository.ApplicationRepository;
import com.campushire.repository.CompanyRepository;
import com.campushire.repository.RecruiterRepository;
import com.campushire.repository.UserRepository;

@Service
public class RecruiterService {

    private final RecruiterRepository recruiterRepository;
    private final CompanyRepository companyRepository;
    private final UserRepository userRepository;
    private final ApplicationRepository applicationRepository;


    public RecruiterService(
            RecruiterRepository recruiterRepository,
            CompanyRepository companyRepository,
            UserRepository userRepository,
            ApplicationRepository applicationRepository) {

        this.recruiterRepository = recruiterRepository;
        this.companyRepository = companyRepository;
        this.userRepository = userRepository;
        this.applicationRepository = applicationRepository;
    }


    // ===========================
    // CREATE RECRUITER PROFILE
    // ===========================

    public RecruiterResponse createProfile(
            String email,
            RecruiterRequest request) {

        User user = getRecruiterUser(email);

        if (recruiterRepository.findByUserId(user.getId()).isPresent()) {
            throw new RuntimeException(
                    "Recruiter profile already exists");
        }


        Company company = companyRepository
                .findById(request.getCompanyId())
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



    // ===========================
    // GET RECRUITER PROFILE
    // ===========================

    public RecruiterResponse getProfile(String email) {

        User user = getRecruiterUser(email);

        Recruiter recruiter = recruiterRepository
                .findByUserId(user.getId())
                .orElseThrow(() ->
                        new RuntimeException(
                                "Recruiter profile not found"));


        return mapToResponse(recruiter);
    }



    // ===========================
    // UPDATE RECRUITER PROFILE
    // ===========================

    public RecruiterResponse updateProfile(
            String email,
            RecruiterRequest request) {


        User user = getRecruiterUser(email);


        Recruiter recruiter = recruiterRepository
                .findByUserId(user.getId())
                .orElseThrow(() ->
                        new RuntimeException(
                                "Recruiter profile not found"));


        Company company = companyRepository
                .findById(request.getCompanyId())
                .orElseThrow(() ->
                        new RuntimeException(
                                "Company not found"));


        recruiter.setCompany(company);
        recruiter.setDesignation(request.getDesignation());
        recruiter.setPhoneNumber(request.getPhoneNumber());


        Recruiter updated = recruiterRepository.save(recruiter);


        return mapToResponse(updated);
    }



    // ===========================
    // GET APPLICANT DETAILS
    // ===========================

    public ApplicantResponse getApplicant(
            String email,
            Long applicationId) {


        User recruiterUser = getRecruiterUser(email);


        Recruiter recruiter = recruiterRepository
                .findByUserId(recruiterUser.getId())
                .orElseThrow(() ->
                        new RuntimeException(
                                "Recruiter profile not found"));



        Application application = applicationRepository
                .findById(applicationId)
                .orElseThrow(() ->
                        new RuntimeException(
                                "Application not found"));



        // Check recruiter belongs to job company

        if (!application.getJob()
                .getCompany()
                .getId()
                .equals(recruiter.getCompany().getId())) {


            throw new RuntimeException(
                    "You are not authorized to view this applicant");
        }



        ApplicantResponse response = new ApplicantResponse();



        response.setApplicationId(
                application.getId());



        response.setStudentName(
                application.getStudent()
                        .getUser()
                        .getFullName());



        response.setEmail(
                application.getStudent()
                        .getUser()
                        .getEmail());



        response.setCollegeRollNo(
                application.getStudent()
                        .getCollegeRollNo());



        response.setPrnNumber(
                application.getStudent()
                        .getPrnNumber());



        // ===========================
        // RESUME RETRIEVAL
        // ===========================

        if (application.getStudent().getResumes() != null
                && !application.getStudent()
                        .getResumes()
                        .isEmpty()) {


            Resume resume =
                    application.getStudent()
                            .getResumes()
                            .stream()
                            .filter(r ->
                                    Boolean.TRUE.equals(
                                            r.getIsDefault()))
                            .findFirst()
                            .orElse(
                                    application.getStudent()
                                            .getResumes()
                                            .get(0)
                            );


            response.setResumeUrl(
                    resume.getPdfUrl()
            );
        }



        response.setApplicationStatus(
                application.getStatus()
                        .name());


        return response;
    }




    // ===========================
    // COMMON ROLE CHECK
    // ===========================

    private User getRecruiterUser(String email) {


        User user = userRepository
                .findByEmail(email)
                .orElseThrow(() ->
                        new RuntimeException(
                                "User not found"));



        if (!user.getRole()
                .name()
                .equals("RECRUITER")) {


            throw new RuntimeException(
                    "Only recruiter users can access recruiter APIs");
        }


        return user;
    }





    // ===========================
    // ENTITY -> DTO
    // ===========================

    private RecruiterResponse mapToResponse(
            Recruiter recruiter) {


        RecruiterResponse response =
                new RecruiterResponse();


        response.setId(
                recruiter.getId());


        response.setUserId(
                recruiter.getUser()
                        .getId());


        response.setEmail(
                recruiter.getUser()
                        .getEmail());


        response.setCompanyName(
                recruiter.getCompany()
                        .getCompanyName());


        response.setDesignation(
                recruiter.getDesignation());


        response.setPhoneNumber(
                recruiter.getPhoneNumber());


        return response;
    }
}