package com.campushire.repository;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import com.campushire.entity.Certification;

public interface CertificationRepository extends JpaRepository<Certification,Long>{
    List<Certification> findByStudentId(Integer studentId);
}