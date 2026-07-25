package com.campushire.service;

import java.util.List;
import java.util.stream.Collectors;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;

import com.campushire.dto.JobRequest;
import com.campushire.dto.JobResponse;
import com.campushire.entity.Company;
import com.campushire.entity.Job;
import com.campushire.entity.Recruiter;
import com.campushire.entity.User;
import com.campushire.repository.CompanyRepository;
import com.campushire.repository.JobRepository;
import com.campushire.repository.RecruiterRepository;
import com.campushire.repository.UserRepository;

@Service
public class JobService {
    private final JobRepository jobRepository;
    private final CompanyRepository companyRepository;
    private final RecruiterRepository recruiterRepository;
    private final UserRepository userRepository;
    private final CurrentUserService currentUserService;
    public JobService(
            JobRepository jobRepository,
            CompanyRepository companyRepository,
            RecruiterRepository recruiterRepository,
            UserRepository userRepository,
            CurrentUserService currentUserService) {
        this.jobRepository = jobRepository;
        this.companyRepository = companyRepository;
        this.recruiterRepository = recruiterRepository;
        this.userRepository = userRepository;
        this.currentUserService = currentUserService;
    }

    // CREATE JOB
    public JobResponse createJob(
            String email,
            JobRequest request) {
        User user = userRepository.findByEmail(email)
                .orElseThrow(() ->
                new RuntimeException("User not found"));
        Recruiter recruiter = recruiterRepository
                .findByUserId(user.getId())
                .orElseThrow(() ->
                new RuntimeException("Recruiter not found"));
        Company company = companyRepository
                .findById(request.getCompanyId())
                .orElseThrow(() ->
                new RuntimeException("Company not found"));
        Job job = new Job();
        job.setTitle(request.getTitle());
        job.setDescription(request.getDescription());
        job.setLocation(request.getLocation());
        job.setSalary(request.getSalary());
        job.setJobType(request.getJobType());
        job.setSkillsRequired(request.getSkillsRequired());
        job.setExperienceRequired(request.getExperienceRequired());
        job.setCompany(company);
        job.setRecruiter(recruiter);
        Job savedJob = jobRepository.save(job);
        return mapToResponse(savedJob);
    }

    // GET ALL JOBS
    public List<JobResponse> getAllJobs() {
        return jobRepository.findAll()
                .stream()
                .map(this::mapToResponse)
                .collect(Collectors.toList());
    }

    // GET JOB BY ID
    public JobResponse getJob(Long id) {
        Job job = jobRepository.findById(id)
                .orElseThrow(() ->
                new RuntimeException("Job not found"));
        return mapToResponse(job);
    }

    // RECRUITER DASHBOARD JOBS
    public List<JobResponse> getRecruiterJobs() {
        User user = currentUserService.getCurrentUser();
        Recruiter recruiter = recruiterRepository
                .findByUserId(user.getId())
                .orElseThrow(() ->
                new RuntimeException("Recruiter not found"));
        return jobRepository.findByRecruiterId(recruiter.getId())
                .stream()
                .map(this::mapToResponse)
                .collect(Collectors.toList());
    }

    // PAGINATION
    public Page<JobResponse> getAllJobs(Pageable pageable) {
        return jobRepository.findAll(pageable)
                .map(this::mapToResponse);
    }

    // SEARCH BY KEYWORD
    public List<JobResponse> searchByKeyword(String keyword) {
        return jobRepository.searchJobs(keyword)
                .stream()
                .map(this::mapToResponse)
                .collect(Collectors.toList());
    }

    // SEARCH BY LOCATION
    public List<JobResponse> searchByLocation(String location) {
        return jobRepository
                .findByLocationContainingIgnoreCase(location)
                .stream()
                .map(this::mapToResponse)
                .collect(Collectors.toList());
    }

    // ENTITY TO DTO
    private JobResponse mapToResponse(Job job) {
        JobResponse response = new JobResponse();
        response.setId(job.getId());
        response.setTitle(job.getTitle());
        response.setDescription(job.getDescription());
        response.setLocation(job.getLocation());
        response.setSalary(job.getSalary());
        response.setJobType(job.getJobType());
        response.setSkillsRequired(job.getSkillsRequired());
        response.setExperienceRequired(job.getExperienceRequired());
        response.setCompanyId(
                job.getCompany().getId()
        );
        response.setCompanyName(
                job.getCompany().getCompanyName()
        );
        response.setRecruiterId(
                job.getRecruiter().getId()
        );
        response.setRecruiterName(
                job.getRecruiter()
                   .getUser()
                   .getFullName()
        );
        response.setCreatedAt(
                job.getCreatedAt()
        );
        return response;
    }
}