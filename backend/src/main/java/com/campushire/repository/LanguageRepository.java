package com.campushire.repository;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import com.campushire.entity.Language;

public interface LanguageRepository extends JpaRepository<Language,Long>{
    List<Language> findByStudentId(Integer studentId);
}