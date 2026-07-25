package com.campushire.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class RecruiterRequest {
    private Long companyId;
    private String designation;
    private String phoneNumber;
}