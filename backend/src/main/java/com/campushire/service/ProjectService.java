package com.campushire.service;

import java.util.List;
import org.springframework.stereotype.Service;
import com.campushire.dto.ProjectRequest;
import com.campushire.dto.ProjectResponse;
import com.campushire.entity.Project;
import com.campushire.entity.Student;
import com.campushire.exception.ResourceNotFoundException;
import com.campushire.repository.ProjectRepository;
import com.campushire.repository.StudentRepository;

@Service
public class ProjectService {
    private final ProjectRepository projectRepository;
    private final StudentRepository studentRepository;
    public ProjectService(ProjectRepository projectRepository, StudentRepository studentRepository) {
        this.projectRepository = projectRepository;
        this.studentRepository = studentRepository;
    }

    public ProjectResponse addProject(ProjectRequest request) {
        Student student = studentRepository.findById(request.getStudentId())
                .orElseThrow(() -> new ResourceNotFoundException("Student not found"));
        Project project = Project.builder()
                .student(student)
                .title(request.getTitle())
                .description(request.getDescription())
                .technologiesUsed(request.getTechnologiesUsed())
                .githubLink(request.getGithubLink())
                .liveDemoLink(request.getLiveDemoLink())
                .projectImageUrl(request.getProjectImageUrl())
                .build();
        return mapToResponse(projectRepository.save(project));
    }

    public List<ProjectResponse> getProjects(Integer studentId) {
        return projectRepository.findByStudentId(studentId)
                .stream()
                .map(this::mapToResponse)
                .toList();
    }
    private ProjectResponse mapToResponse(Project project) {
        return ProjectResponse.builder()
                .id(project.getId())
                .title(project.getTitle())
                .description(project.getDescription())
                .technologiesUsed(project.getTechnologiesUsed())
                .githubLink(project.getGithubLink())
                .liveDemoLink(project.getLiveDemoLink())
                .projectImageUrl(project.getProjectImageUrl())
                .build();
    }
}