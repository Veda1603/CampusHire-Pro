package com.campushire.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class DocumentRequest{
    private Integer studentId;
    private String documentType;
    private String documentName;
    private String fileUrl;
}