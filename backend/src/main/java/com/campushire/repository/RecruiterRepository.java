package com.campushire.repository;

import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;

import com.campushire.entity.Recruiter;

public interface RecruiterRepository extends JpaRepository<Recruiter, Long> {

    Optional<Recruiter> findByUserId(Long userId);

}