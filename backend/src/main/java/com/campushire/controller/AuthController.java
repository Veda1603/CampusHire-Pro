package com.campushire.controller;

import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.campushire.dto.LoginRequest;
import com.campushire.dto.LoginResponse;
import com.campushire.dto.RegisterRequest;
import com.campushire.dto.UserResponse;
import com.campushire.service.AuthService;

@RestController
@RequestMapping("/api/auth")
public class AuthController {
    private final AuthService authService;
    public AuthController(AuthService authService){
        this.authService = authService;
    }
    @PostMapping("/register")
    public UserResponse register(@RequestBody RegisterRequest request){
        return authService.register(request);
    }
    
    @PostMapping("/login")
    public LoginResponse login(
            @RequestBody LoginRequest request){
        return authService.login(request);
    }
}