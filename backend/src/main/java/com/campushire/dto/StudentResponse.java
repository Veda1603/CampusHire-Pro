package com.campushire.dto;

import java.math.BigDecimal;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class StudentResponse {
    private Integer id;
    private String fullName;
    private String email;
    private String collegeRollNo;
    private String prnNumber;
    private Integer currentYear;
    private BigDecimal currentCGPA;
    private Integer expectedPassoutYear;
    private Boolean profileCompleted;
    private String verificationStatus;
}