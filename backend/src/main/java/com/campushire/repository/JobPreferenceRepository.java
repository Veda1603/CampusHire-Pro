package com.campushire.repository;
import org.springframework.data.jpa.repository.JpaRepository;
import com.campushire.entity.JobPreference;

public interface JobPreferenceRepository extends JpaRepository<JobPreference,Long>{
    JobPreference findByStudentId(Integer studentId);
}