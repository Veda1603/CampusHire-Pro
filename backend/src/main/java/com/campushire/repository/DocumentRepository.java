package com.campushire.repository;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import com.campushire.entity.Document;

public interface DocumentRepository extends JpaRepository<Document,Long>{
    List<Document> findByStudentId(Integer studentId);
}