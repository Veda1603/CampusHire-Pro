package com.campushire.repository;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import com.campushire.entity.Resume;

public interface ResumeRepository extends JpaRepository<Resume,Long>{
    List<Resume> findByStudentId(Integer studentId);
}