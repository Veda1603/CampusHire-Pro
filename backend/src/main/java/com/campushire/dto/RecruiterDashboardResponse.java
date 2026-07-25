package com.campushire.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class RecruiterDashboardResponse {
    private String companyName;
    private Long totalJobs;
    private Long totalApplications;
    private Long shortlisted;
    private Long rejected;
}