package com.campushire.service;

import java.util.List;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;
import com.campushire.dto.interview.InterviewRequestDTO;
import com.campushire.dto.interview.InterviewResponseDTO;
import com.campushire.entity.Application;
import com.campushire.entity.Interview;
import com.campushire.entity.InterviewStatus;
import com.campushire.entity.NotificationType;
import com.campushire.entity.User;
import com.campushire.entity.UserRole;
import com.campushire.repository.ApplicationRepository;
import com.campushire.repository.InterviewRepository;
import com.campushire.repository.UserRepository;
import lombok.RequiredArgsConstructor;

@Service
@RequiredArgsConstructor
public class InterviewService {

    private final InterviewRepository interviewRepository;
    private final ApplicationRepository applicationRepository;
    private final UserRepository userRepository;
    private final NotificationService notificationService;

    public InterviewResponseDTO createInterview(InterviewRequestDTO request) {
        Application application = applicationRepository.findById(request.getApplicationId())
                .orElseThrow(() -> new RuntimeException("Application not found"));
        Interview interview = new Interview();
        interview.setApplication(application);
        interview.setInterviewDateTime(request.getInterviewDateTime());
        interview.setMode(request.getMode());
        interview.setMeetingLink(request.getMeetingLink());
        interview.setStatus(InterviewStatus.SCHEDULED);
        Interview savedInterview = interviewRepository.save(interview);
        notificationService.createNotification(
                application.getStudent().getUser(),
                "Interview scheduled for " + application.getJob().getTitle(),
                NotificationType.INTERVIEW_SCHEDULED
        );
        return convertToDTO(savedInterview);
    }

    public List<InterviewResponseDTO> getAllInterviews() {
        User user = getCurrentUser();
        List<Interview> interviews = user.getRole() == UserRole.STUDENT
                ? interviewRepository.findByApplicationStudentUserEmail(user.getEmail())
                : interviewRepository.findAll();
        return interviews.stream().map(this::convertToDTO).toList();
    }

    public InterviewResponseDTO getInterviewById(Long id) {
        User user = getCurrentUser();
        Interview interview = interviewRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("Interview not found"));
        if (user.getRole() == UserRole.STUDENT &&
                !interview.getApplication().getStudent().getUser().getEmail().equals(user.getEmail())) {
            throw new RuntimeException("Access denied");
        }
        return convertToDTO(interview);
    }

    public List<InterviewResponseDTO> getByApplication(Long applicationId) {
        User user = getCurrentUser();
        Application application = applicationRepository.findById(applicationId)
                .orElseThrow(() -> new RuntimeException("Application not found"));
        if (user.getRole() == UserRole.STUDENT &&
                !application.getStudent().getUser().getEmail().equals(user.getEmail())) {
            throw new RuntimeException("Access denied");
        }
        return interviewRepository.findByApplicationId(applicationId)
                .stream()
                .map(this::convertToDTO)
                .toList();
    }

    public InterviewResponseDTO updateStatus(Long id, InterviewStatus status) {
        Interview interview = interviewRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("Interview not found"));
        interview.setStatus(status);
        Interview updated = interviewRepository.save(interview);
        notificationService.createNotification(
                interview.getApplication().getStudent().getUser(),
                "Interview status updated to " + status,
                NotificationType.INTERVIEW_UPDATE
        );
        return convertToDTO(updated);
    }

    private User getCurrentUser() {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        return userRepository.findByEmail(authentication.getName())
                .orElseThrow(() -> new RuntimeException("User not found"));
    }

    private InterviewResponseDTO convertToDTO(Interview interview) {
        InterviewResponseDTO dto = new InterviewResponseDTO();
        dto.setId(interview.getId());
        dto.setApplicationId(interview.getApplication().getId());
        dto.setInterviewDateTime(interview.getInterviewDateTime());
        dto.setMode(interview.getMode());
        dto.setStatus(interview.getStatus());
        dto.setMeetingLink(interview.getMeetingLink());
        return dto;
    }
}