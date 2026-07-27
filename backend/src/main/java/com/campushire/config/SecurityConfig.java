package com.campushire.config;

import com.campushire.security.JwtAuthenticationFilter;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import org.springframework.http.HttpMethod;

import org.springframework.security.config.Customizer;
import org.springframework.security.config.annotation.method.configuration.EnableMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;

import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;

import org.springframework.security.config.http.SessionCreationPolicy;

import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;

import org.springframework.security.web.SecurityFilterChain;

import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;

@Configuration
@EnableWebSecurity
@EnableMethodSecurity

public class SecurityConfig {
    private final JwtAuthenticationFilter jwtAuthenticationFilter;
    public SecurityConfig(
            JwtAuthenticationFilter jwtAuthenticationFilter) {
        this.jwtAuthenticationFilter = jwtAuthenticationFilter;
    }
    @Bean
    public PasswordEncoder passwordEncoder() {
        return new BCryptPasswordEncoder();
    }
    @Bean
    public SecurityFilterChain securityFilterChain(
            HttpSecurity http) throws Exception {
        http
        .csrf(csrf -> csrf.disable())
        .cors(Customizer.withDefaults())
        .sessionManagement(session ->
                session.sessionCreationPolicy(
                        SessionCreationPolicy.STATELESS)
        )
        .authorizeHttpRequests(auth -> auth
        		.requestMatchers(
        			    "/swagger-ui/**",
        			    "/v3/api-docs/**"
        			).permitAll()
                // AUTH
                .requestMatchers(
                        "/api/auth/**")
                .permitAll()
                // JOBS PUBLIC
                .requestMatchers(
                        HttpMethod.GET,
                        "/api/jobs/**")
                .permitAll()
                // COMPANY APIs
                // Recruiter can GET company details
                .requestMatchers(
                        HttpMethod.GET,
                        "/api/company/**")
               .authenticated()
                // Admin only create company
                .requestMatchers(
                        HttpMethod.POST,
                        "/api/company/**")
                .hasRole("ADMIN")
                // Admin only update company
                .requestMatchers(
                        HttpMethod.PUT,
                        "/api/company/**")
                .hasRole("ADMIN")



                // Recruiter APIs
                .requestMatchers(
                        "/api/recruiter/**")
                .hasRole("RECRUITER")
                // Student update
                .requestMatchers(
                        HttpMethod.PUT,
                        "/api/student/**")
                .hasRole("STUDENT")
                .anyRequest()
                .authenticated()
        )
        .addFilterBefore(
                jwtAuthenticationFilter,
                UsernamePasswordAuthenticationFilter.class
        );
        return http.build();
    }
}