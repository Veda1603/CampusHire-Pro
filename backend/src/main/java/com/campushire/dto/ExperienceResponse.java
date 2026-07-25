package com.campushire.dto;

import java.time.LocalDate;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class ExperienceResponse {
    private Long id;
    private String experienceType;
    private String companyName;
    private String jobTitle;
    private LocalDate startDate;
    private LocalDate endDate;
    private Boolean currentlyWorking;
    private String experienceLetterUrl;

}