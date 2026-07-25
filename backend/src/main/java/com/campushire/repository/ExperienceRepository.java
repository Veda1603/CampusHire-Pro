package com.campushire.repository;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import com.campushire.entity.Experience;

public interface ExperienceRepository extends JpaRepository<Experience, Long> {
    List<Experience> findByStudentId(Integer studentId);
}