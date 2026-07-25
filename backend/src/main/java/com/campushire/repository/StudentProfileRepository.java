package com.campushire.repository;

import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import com.campushire.entity.StudentProfile;

public interface StudentProfileRepository extends JpaRepository<StudentProfile, Long> {
    Optional<StudentProfile> findByStudentId(Integer studentId);
}