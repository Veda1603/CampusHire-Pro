package com.campushire.service;

import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;

import com.campushire.entity.User;
import com.campushire.repository.UserRepository;

@Service
public class CurrentUserService {
    private final UserRepository userRepository;
    public CurrentUserService(UserRepository userRepository) {
        this.userRepository = userRepository;
    }
    public User getCurrentUser() {
        Object principal = SecurityContextHolder
                .getContext()
                .getAuthentication()
                .getPrincipal();
        String email;
        if(principal instanceof UserDetails) {
            email = ((UserDetails) principal).getUsername();
        }
        else {
            email = principal.toString();
        }
        return userRepository.findByEmail(email)
                .orElseThrow(() ->
                    new RuntimeException("User not found")
                );
    }
}