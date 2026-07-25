package com.campushire.service;

import java.util.List;

import org.springframework.stereotype.Service;

import com.campushire.dto.SkillRequest;
import com.campushire.dto.SkillResponse;
import com.campushire.entity.Skill;
import com.campushire.entity.Student;
import com.campushire.exception.ResourceNotFoundException;
import com.campushire.repository.SkillRepository;
import com.campushire.repository.StudentRepository;

@Service
public class SkillService {
    private final SkillRepository skillRepository;
    private final StudentRepository studentRepository;
    public SkillService(SkillRepository skillRepository, StudentRepository studentRepository) {
        this.skillRepository = skillRepository;
        this.studentRepository = studentRepository;
    }
    public SkillResponse addSkill(SkillRequest request) {
        Student student = studentRepository.findById(request.getStudentId())
                .orElseThrow(() -> new ResourceNotFoundException("Student not found"));
        Skill skill = Skill.builder()
                .student(student)
                .skillName(request.getSkillName())
                .proficiency(request.getProficiency())
                .build();
        return mapToResponse(skillRepository.save(skill));
    }
    public List<SkillResponse> getSkills(Integer studentId) {
        return skillRepository.findByStudentId(studentId)
                .stream()
                .map(this::mapToResponse)
                .toList();
    }
    private SkillResponse mapToResponse(Skill skill) {
        return SkillResponse.builder()
                .id(skill.getId())
                .skillName(skill.getSkillName())
                .proficiency(skill.getProficiency())
                .build();
    }
}