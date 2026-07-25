package com.campushire.service;

import com.campushire.dto.LoginRequest;
import com.campushire.dto.LoginResponse;
import com.campushire.dto.RegisterRequest;
import com.campushire.entity.User;
import com.campushire.entity.UserRole;
import com.campushire.dto.UserResponse;
import com.campushire.repository.UserRepository;
import com.campushire.security.JwtService;

import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
@Service
public class AuthService {

    private final UserRepository userRepository;
    private final PasswordEncoder passwordEncoder;

    private final JwtService jwtService;
    public AuthService(
            UserRepository userRepository,
            PasswordEncoder passwordEncoder,
            JwtService jwtService){

        this.userRepository=userRepository;
        this.passwordEncoder=passwordEncoder;
        this.jwtService=jwtService;
    }
   
    
    public UserResponse register(RegisterRequest request) {

    	User user = User.builder()
    	        .fullName(request.getFullName())
    	        .email(request.getEmail())
    	        .passwordHash(passwordEncoder.encode(request.getPassword()))
    	        .role(UserRole.valueOf(request.getRole()))
    	        .active(true)
    	        .build();


        User savedUser = userRepository.save(user);


        UserResponse response = new UserResponse();

        response.setId(savedUser.getId());
        response.setEmail(savedUser.getEmail());
        response.setFullName(savedUser.getFullName());
        response.setRole(savedUser.getRole().name());
        response.setActive(savedUser.getActive());


        return response;
    }
    public LoginResponse login(LoginRequest request){


        User user = userRepository
                .findByEmail(request.getEmail())
                .orElseThrow(
                  () -> new RuntimeException("User not found")
                );


        if(!passwordEncoder.matches(
                request.getPassword(),
                user.getPasswordHash())){

            throw new RuntimeException("Invalid password");
        }


        String token =
            jwtService.generateToken(user.getEmail());


        LoginResponse response =
            new LoginResponse();


        response.setToken(token);
        response.setId(user.getId());
        response.setEmail(user.getEmail());
        response.setFullName(user.getFullName());
        response.setRole(user.getRole().name());


        return response;
    }
}