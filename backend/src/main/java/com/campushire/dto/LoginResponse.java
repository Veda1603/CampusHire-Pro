package com.campushire.dto;

import lombok.Data;

@Data
public class LoginResponse {

    private String token;

    private Long id;

    private String email;

    private String fullName;

    private String role;
}