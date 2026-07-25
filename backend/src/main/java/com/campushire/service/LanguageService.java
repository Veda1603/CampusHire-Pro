package com.campushire.service;

import java.util.List;
import org.springframework.stereotype.Service;
import com.campushire.dto.LanguageRequest;
import com.campushire.dto.LanguageResponse;
import com.campushire.entity.Language;
import com.campushire.entity.Student;
import com.campushire.exception.ResourceNotFoundException;
import com.campushire.repository.LanguageRepository;
import com.campushire.repository.StudentRepository;

@Service
public class LanguageService {
    private final LanguageRepository languageRepository;
    private final StudentRepository studentRepository;
    public LanguageService(LanguageRepository languageRepository, StudentRepository studentRepository) {
        this.languageRepository = languageRepository;
        this.studentRepository = studentRepository;
    }
    public LanguageResponse addLanguage(LanguageRequest request) {
        Student student = studentRepository.findById(request.getStudentId())
                .orElseThrow(() -> new ResourceNotFoundException("Student not found"));
        Language language = Language.builder()
                .student(student)
                .languageName(request.getLanguageName())
                .proficiency(request.getProficiency())
                .build();
        return mapToResponse(languageRepository.save(language));
    }
    public List<LanguageResponse> getLanguages(Integer studentId) {
        return languageRepository.findByStudentId(studentId)
                .stream()
                .map(this::mapToResponse)
                .toList();
    }

    private LanguageResponse mapToResponse(Language language) {
        return LanguageResponse.builder()
                .id(language.getId())
                .languageName(language.getLanguageName())
                .proficiency(language.getProficiency())
                .build();
    }
}