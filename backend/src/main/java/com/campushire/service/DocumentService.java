package com.campushire.service;

import java.util.List;
import org.springframework.stereotype.Service;
import com.campushire.dto.*;
import com.campushire.entity.*;
import com.campushire.repository.*;
import com.campushire.exception.ResourceNotFoundException;

@Service
public class DocumentService {

    private final DocumentRepository documentRepository;
    private final StudentRepository studentRepository;

    public DocumentService(DocumentRepository documentRepository,StudentRepository studentRepository){
        this.documentRepository=documentRepository;
        this.studentRepository=studentRepository;
    }

    public DocumentResponse add(DocumentRequest request){

        Student student=studentRepository.findById(request.getStudentId())
                .orElseThrow(()->new ResourceNotFoundException("Student not found"));

        Document document=Document.builder()
                .student(student)
                .documentType(request.getDocumentType())
                .documentName(request.getDocumentName())
                .fileUrl(request.getFileUrl())
                .build();

        return map(documentRepository.save(document));
    }

    public List<DocumentResponse> get(Integer studentId){
        return documentRepository.findByStudentId(studentId)
                .stream().map(this::map).toList();
    }

    private DocumentResponse map(Document d){
        return DocumentResponse.builder()
                .id(d.getId())
                .documentType(d.getDocumentType())
                .documentName(d.getDocumentName())
                .fileUrl(d.getFileUrl())
                .build();
    }
}