package com.campushire.dto;
import java.time.LocalDate;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class CertificationResponse{
    private Long id;
    private String title;
    private String issuingAuthority;
    private LocalDate issueDate;
    private String certificateUrl;
}