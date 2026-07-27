package com.campushire.dto.interview;

import java.time.LocalDateTime;

import com.campushire.entity.InterviewMode;
import com.campushire.entity.InterviewStatus;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class InterviewResponseDTO {
    private Long id;
    private Long applicationId;
    private LocalDateTime interviewDateTime;
    private InterviewMode mode;
    private InterviewStatus status;
    private String meetingLink;
}