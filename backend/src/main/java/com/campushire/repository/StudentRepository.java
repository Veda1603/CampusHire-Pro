package com.campushire.repository;

import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import com.campushire.entity.Student;

public interface StudentRepository extends JpaRepository<Student, Integer> {
    Optional<Student> findByUserId(Long userId);
}