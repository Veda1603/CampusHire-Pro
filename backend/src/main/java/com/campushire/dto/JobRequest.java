package com.campushire.dto;

import java.math.BigDecimal;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class JobRequest {
    private String title;
    private String description;
    private String location;
    private BigDecimal salary;
    private String jobType;
    private String skillsRequired;
    private Integer experienceRequired;
    private Long companyId;

}