package com.campushire.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class ApplicationStatsResponse {
    private Long totalApplications;
    private Long applied;
    private Long shortlisted;
    private Long rejected;
}