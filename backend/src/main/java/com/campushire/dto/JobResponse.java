package com.campushire.dto;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class JobResponse {
    private Long id;
    private String title;
    private String description;
    private String location;
    private BigDecimal salary;
    private String jobType;
    private String skillsRequired;
    private Integer experienceRequired;
    private Long companyId;
    private String companyName;
    private Long recruiterId;
    private String recruiterName;
    private LocalDateTime createdAt;
}