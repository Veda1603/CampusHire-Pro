package com.campushire.dto;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class ResumeResponse{
    private Long id;
    private String resumeName;
    private String templateName;
    private String pdfUrl;
    private String docxUrl;
    private Boolean isDefault;
}