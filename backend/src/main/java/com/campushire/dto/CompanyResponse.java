package com.campushire.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class CompanyResponse {
    private Long id;
    private String companyName;
    private String industry;
    private String location;
    private String website;
    private String description;
}