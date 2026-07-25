package com.campushire.dto;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class JobPreferenceRequest{
    private Integer studentId;
    private String preferredJobType;
    private String preferredLocation;
    private String preferredIndustry;
    private Double expectedSalary;
    private Boolean openToRelocation;
}