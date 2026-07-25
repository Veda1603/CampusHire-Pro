package com.campushire.dto;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class LanguageRequest{
    private Integer studentId;
    private String languageName;
    private String proficiency;
}