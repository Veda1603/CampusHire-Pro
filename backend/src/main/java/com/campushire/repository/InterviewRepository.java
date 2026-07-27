package com.campushire.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.campushire.entity.Interview;
import com.campushire.entity.InterviewStatus;

@Repository
public interface InterviewRepository extends JpaRepository<Interview, Long> {
    List<Interview> findByStatus(InterviewStatus status);
    List<Interview> findByApplicationId(Long applicationId);
    List<Interview> findByApplicationStudentUserEmail(String email);
}