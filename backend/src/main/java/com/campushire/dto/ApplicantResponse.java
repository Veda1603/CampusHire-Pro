package com.campushire.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class ApplicantResponse {

    private Long applicationId;

    private String studentName;

    private String email;

    private String collegeRollNo;

    private String prnNumber;

    private String resumeUrl;

    private String applicationStatus;
}