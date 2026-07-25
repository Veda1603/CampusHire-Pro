package com.campushire.dto;

import java.math.BigDecimal;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class EducationResponse {
    private Long id;
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