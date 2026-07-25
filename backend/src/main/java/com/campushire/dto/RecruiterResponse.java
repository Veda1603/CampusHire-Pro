package com.campushire.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class RecruiterResponse {
    private Long id;
    private Long userId;
    private String email;
    private String companyName;
    private String designation;
    private String phoneNumber;
}