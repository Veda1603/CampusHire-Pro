package com.campushire.dto;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class DocumentResponse{
    private Long id;
    private String documentType;
    private String documentName;
    private String fileUrl;
}