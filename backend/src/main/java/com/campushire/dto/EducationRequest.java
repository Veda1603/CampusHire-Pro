package com.campushire.dto;

import java.math.BigDecimal;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class EducationRequest {
    private Integer studentId;
    private String qualification;
    private String collegeName;
    private String university;
    private String course;
    private String stream;
    private String gradingSystem;
    private BigDecimal score;
    private Integer passoutYear;
    private String marksheetUrl;
}