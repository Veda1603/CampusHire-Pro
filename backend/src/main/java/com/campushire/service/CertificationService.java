package com.campushire.service;

import java.util.List;
import org.springframework.stereotype.Service;
import com.campushire.dto.CertificationRequest;
import com.campushire.dto.CertificationResponse;
import com.campushire.entity.Certification;
import com.campushire.entity.Student;
import com.campushire.exception.ResourceNotFoundException;
import com.campushire.repository.CertificationRepository;
import com.campushire.repository.StudentRepository;

@Service
public class CertificationService {
    private final CertificationRepository certificationRepository;
    private final StudentRepository studentRepository;
    public CertificationService(CertificationRepository certificationRepository, StudentRepository studentRepository) {
        this.certificationRepository = certificationRepository;
        this.studentRepository = studentRepository;
    }
    public CertificationResponse addCertification(CertificationRequest request) {
        Student student = studentRepository.findById(request.getStudentId())
                .orElseThrow(() -> new ResourceNotFoundException("Student not found"));
        Certification certification = Certification.builder()
                .student(student)
                .title(request.getTitle())
                .issuingAuthority(request.getIssuingAuthority())
                .issueDate(request.getIssueDate())
                .certificateUrl(request.getCertificateUrl())
                .build();
        return mapToResponse(certificationRepository.save(certification));
    }
    public List<CertificationResponse> getCertifications(Integer studentId) {
        return certificationRepository.findByStudentId(studentId)
                .stream()
                .map(this::mapToResponse)
                .toList();
    }
    private CertificationResponse mapToResponse(Certification certification) {
        return CertificationResponse.builder()
                .id(certification.getId())
                .title(certification.getTitle())
                .issuingAuthority(certification.getIssuingAuthority())
                .issueDate(certification.getIssueDate())
                .certificateUrl(certification.getCertificateUrl())
                .build();
    }
}