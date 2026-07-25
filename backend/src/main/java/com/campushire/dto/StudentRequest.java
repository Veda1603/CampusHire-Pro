package com.campushire.dto;

import java.math.BigDecimal;

import jakarta.validation.constraints.DecimalMax;
import jakarta.validation.constraints.DecimalMin;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class StudentRequest {
    private Long userId;
    @NotBlank(message = "College roll number is required")
    private String collegeRollNo;
    @NotBlank(message = "PRN number is required")
    private String prnNumber;
    @NotNull(message = "Current year is required")
    private Integer currentYear;
    @NotNull(message = "CGPA is required")
    @DecimalMin(value = "0.0", message = "CGPA cannot be negative")
    @DecimalMax(value = "10.0", message = "CGPA cannot be greater than 10")
    private BigDecimal currentCGPA;
    @NotNull(message = "Expected passout year is required")
    private Integer expectedPassoutYear;
}