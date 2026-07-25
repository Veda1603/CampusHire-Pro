package com.campushire.repository;
import org.springframework.data.jpa.repository.JpaRepository;
import com.campushire.entity.SocialLink;

public interface SocialLinkRepository extends JpaRepository<SocialLink,Long>{
    SocialLink findByStudentId(Integer studentId);
}