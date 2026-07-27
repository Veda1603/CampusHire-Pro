package com.campushire.dto;
import jakarta.validation.constraints.NotBlank;
import lombok.Getter;
import lombok.Setter;
@Getter
@Setter
public class CompanyRequest {
    @NotBlank(message="Company name is required")
    private String companyName;
    private String industry;
    private String location;
    private String website;
    private String description;
}