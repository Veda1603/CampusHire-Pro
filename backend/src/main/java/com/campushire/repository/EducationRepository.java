package com.campushire.repository;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import com.campushire.entity.Education;

public interface EducationRepository extends JpaRepository<Education, Long> {
    List<Education> findByStudentId(Integer studentId);
}