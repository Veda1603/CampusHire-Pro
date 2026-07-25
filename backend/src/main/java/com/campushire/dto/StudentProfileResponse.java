package com.campushire.dto;

import java.time.LocalDate;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class StudentProfileResponse {

    private Long id;
    private String firstName;
    private String lastName;
    private String phoneNumber;
    private LocalDate dateOfBirth;
    private String gender;
    private String nationality;
    private String maritalStatus;
    private String addressLine1;
    private String addressLine2;
    private String city;
    private String taluka;
    private String district;
    private String state;
    private String country;
    private String pincode;
    private String profilePhoto;

}