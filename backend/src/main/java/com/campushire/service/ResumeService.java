package com.campushire.service;

import java.util.List;
import org.springframework.stereotype.Service;
import com.campushire.dto.*;
import com.campushire.entity.*;
import com.campushire.repository.*;
import com.campushire.exception.ResourceNotFoundException;

@Service
public class ResumeService {

    private final ResumeRepository resumeRepository;
    private final StudentRepository studentRepository;

    public ResumeService(ResumeRepository resumeRepository,StudentRepository studentRepository){
        this.resumeRepository=resumeRepository;
        this.studentRepository=studentRepository;
    }

    public ResumeResponse add(ResumeRequest request){

        Student student=studentRepository.findById(request.getStudentId())
                .orElseThrow(()->new ResourceNotFoundException("Student not found"));

        Resume resume=Resume.builder()
                .student(student)
                .resumeName(request.getResumeName())
                .templateName(request.getTemplateName())
                .pdfUrl(request.getPdfUrl())
                .docxUrl(request.getDocxUrl())
                .isDefault(request.getIsDefault())
                .build();

        return map(resumeRepository.save(resume));
    }

    public List<ResumeResponse> get(Integer studentId){
        return resumeRepository.findByStudentId(studentId)
                .stream().map(this::map).toList();
    }

    private ResumeResponse map(Resume r){
        return ResumeResponse.builder()
                .id(r.getId())
                .resumeName(r.getResumeName())
                .templateName(r.getTemplateName())
                .pdfUrl(r.getPdfUrl())
                .docxUrl(r.getDocxUrl())
                .isDefault(r.getIsDefault())
                .build();
    }
}