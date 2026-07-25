package com.campushire.dto;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class JobPreferenceResponse{
    private Long id;
    private String preferredJobType;
    private String preferredLocation;
    private String preferredIndustry;
    private Double expectedSalary;
    private Boolean openToRelocation;
}