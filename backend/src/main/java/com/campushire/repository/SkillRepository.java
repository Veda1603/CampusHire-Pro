package com.campushire.repository;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import com.campushire.entity.Skill;

public interface SkillRepository extends JpaRepository<Skill, Long> {
    List<Skill> findByStudentId(Integer studentId);
}