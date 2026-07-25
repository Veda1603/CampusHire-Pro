package com.campushire.dto;
import java.time.LocalDate;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class CertificationRequest{
    private Integer studentId;
    private String title;
    private String issuingAuthority;
    private LocalDate issueDate;
    private String certificateUrl;
}