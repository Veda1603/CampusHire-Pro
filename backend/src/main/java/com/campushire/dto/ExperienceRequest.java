package com.campushire.dto;

import java.time.LocalDate;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class ExperienceRequest {
    private Integer studentId;
    private String experienceType;
    private String companyName;
    private String jobTitle;
    private LocalDate startDate;
    private LocalDate endDate;
    private Boolean currentlyWorking;
    private String experienceLetterUrl;

}