package com.campushire.service;

import org.springframework.stereotype.Service;
import com.campushire.dto.RecruiterDashboardResponse;
import com.campushire.entity.Recruiter;
import com.campushire.entity.User;
import com.campushire.repository.ApplicationRepository;
import com.campushire.repository.JobRepository;
import com.campushire.repository.RecruiterRepository;
import java.util.List;
import com.campushire.entity.Application;
import com.campushire.entity.ApplicationStatus;

@Service
public class RecruiterDashboardService {
    private final RecruiterRepository recruiterRepository;
    private final JobRepository jobRepository;
    private final ApplicationRepository applicationRepository;
    private final CurrentUserService currentUserService;

    public RecruiterDashboardService(
            RecruiterRepository recruiterRepository,
            JobRepository jobRepository,
            ApplicationRepository applicationRepository,
            CurrentUserService currentUserService) {
        this.recruiterRepository = recruiterRepository;
        this.jobRepository = jobRepository;
        this.applicationRepository = applicationRepository;
        this.currentUserService = currentUserService;
    }

    public RecruiterDashboardResponse getDashboard() {
        User user = currentUserService.getCurrentUser();

        Recruiter recruiter = recruiterRepository.findByUserId(user.getId())
                .orElseThrow(() -> new RuntimeException("Recruiter profile not found"));

        List<Application> applications =
                applicationRepository.findByJob_Recruiter_Id(recruiter.getId());

        RecruiterDashboardResponse response = new RecruiterDashboardResponse();

        response.setCompanyName(
                recruiter.getCompany().getCompanyName()
        );

        response.setTotalJobs(
                (long) jobRepository.findByRecruiterId(recruiter.getId()).size()
        );

        response.setTotalApplications(
                (long) applications.size()
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
}