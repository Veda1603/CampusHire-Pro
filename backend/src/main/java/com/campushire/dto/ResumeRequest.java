package com.campushire.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class ResumeRequest{
    private Integer studentId;
    private String resumeName;
    private String templateName;
    private String pdfUrl;
    private String docxUrl;
    private Boolean isDefault;
}