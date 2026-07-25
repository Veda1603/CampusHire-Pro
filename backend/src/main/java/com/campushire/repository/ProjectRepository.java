package com.campushire.repository;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import com.campushire.entity.Project;

public interface ProjectRepository extends JpaRepository<Project,Long>{
    List<Project> findByStudentId(Integer studentId);
}