package com.campushire.service;

import java.util.List;
import java.util.stream.Collectors;
import org.springframework.stereotype.Service;
import com.campushire.dto.ApplicationRequest;
import com.campushire.dto.ApplicationResponse;
import com.campushire.dto.ApplicationStatsResponse;
import com.campushire.entity.Application;
import com.campushire.entity.ApplicationStatus;
import com.campushire.entity.Job;
import com.campushire.entity.Student;
import com.campushire.entity.User;
import com.campushire.entity.Recruiter;
import com.campushire.repository.ApplicationRepository;
import com.campushire.repository.JobRepository;
import com.campushire.repository.StudentRepository;
import com.campushire.repository.RecruiterRepository;
import com.campushire.service.EmailService;

@Service
public class ApplicationService {
    private final ApplicationRepository applicationRepository;
    private final JobRepository jobRepository;
    private final StudentRepository studentRepository;
    private final CurrentUserService currentUserService;
    private final RecruiterRepository recruiterRepository;
    private final EmailService emailService;
    public ApplicationService(
            ApplicationRepository applicationRepository,
            JobRepository jobRepository,
            StudentRepository studentRepository,
            CurrentUserService currentUserService,
            RecruiterRepository recruiterRepository,
            EmailService emailService) {
        this.applicationRepository = applicationRepository;
        this.jobRepository = jobRepository;
        this.studentRepository = studentRepository;
        this.currentUserService = currentUserService;
        this.recruiterRepository = recruiterRepository;
        this.emailService = emailService;
    }

    public ApplicationResponse applyJob(ApplicationRequest request) {
        Job job = jobRepository.findById(request.getJobId())
                .orElseThrow(() -> new RuntimeException("Job not found"));
        User user = currentUserService.getCurrentUser();
        Student student = studentRepository.findByUserId(user.getId())
                .orElseThrow(() -> new RuntimeException("Student profile not found"));
        Application application = new Application();
        application.setJob(job);
        application.setStudent(student);
        application.setStatus(ApplicationStatus.APPLIED);
        Application savedApplication = applicationRepository.save(application);
        emailService.sendEmail(
                job.getRecruiter()
                .getUser()
                .getEmail(),
                "New Job Application Received",
                student.getUser().getFullName()
                + " applied for "
                + job.getTitle()
                + " position."
        );
        return convertToResponse(savedApplication);
    }

    public List<ApplicationResponse> getStudentApplications() {
        User user = currentUserService.getCurrentUser();
        Student student = studentRepository.findByUserId(user.getId())
                .orElseThrow(() -> new RuntimeException("Student profile not found"));
        return applicationRepository.findByStudent(student)
                .stream()
                .map(this::convertToResponse)
                .collect(Collectors.toList());
    }

    public List<ApplicationResponse> getCompanyApplications() {
        User user = currentUserService.getCurrentUser();
        Recruiter recruiter = recruiterRepository.findByUserId(user.getId())
                .orElseThrow(() -> new RuntimeException("Recruiter profile not found"));
        return applicationRepository.findByJob_Recruiter_Id(recruiter.getId())
                .stream()
                .map(this::convertToResponse)
                .collect(Collectors.toList());
    }

    public ApplicationResponse updateApplicationStatus(Long id, String status) {
        User user = currentUserService.getCurrentUser();
        Recruiter recruiter = recruiterRepository.findByUserId(user.getId())
                .orElseThrow(() -> new RuntimeException("Recruiter profile not found"));
        Application application = applicationRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("Application not found"));
        if (!application.getJob().getRecruiter().getId().equals(recruiter.getId())) {
            throw new RuntimeException("You cannot update this application");
        }
        application.setStatus(ApplicationStatus.valueOf(status));
        Application updatedApplication = applicationRepository.save(application);
        emailService.sendEmail(
                application.getStudent()
                .getUser()
                .getEmail(),
                "Application Status Updated",
                "Your application for "
                + application.getJob().getTitle()
                + " has been updated to "
                + application.getStatus()
        );
        return convertToResponse(updatedApplication);
    }
    
    public ApplicationStatsResponse getApplicationStats() {
        User user = currentUserService.getCurrentUser();
        Recruiter recruiter = recruiterRepository.findByUserId(user.getId())
                .orElseThrow(() -> new RuntimeException("Recruiter profile not found"));
        List<Application> applications = applicationRepository.findByJob_Recruiter_Id(recruiter.getId());
        ApplicationStatsResponse response = new ApplicationStatsResponse();
        response.setTotalApplications((long) applications.size());
        response.setApplied(
                applications.stream()
                .filter(app -> app.getStatus() == ApplicationStatus.APPLIED)
                .count()
        );
        response.setShortlisted(
                applications.stream()
                .filter(app -> app.getStatus() == ApplicationStatus.SHORTLISTED)
                .count()
        );
        response.setRejected(
                applications.stream()
                .filter(app -> app.getStatus() == ApplicationStatus.REJECTED)
                .count()
        );
        return response;
    }
    private ApplicationResponse convertToResponse(Application application) {
        ApplicationResponse response = new ApplicationResponse();
        response.setId(application.getId());
        response.setJobTitle(application.getJob().getTitle());
        response.setCompanyName(application.getJob().getCompany().getCompanyName());
        response.setStatus(application.getStatus());
        response.setAppliedAt(application.getAppliedAt());
        return response;
    }
}