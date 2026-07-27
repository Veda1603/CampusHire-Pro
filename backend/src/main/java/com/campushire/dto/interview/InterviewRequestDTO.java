package com.campushire.dto.interview;
import java.time.LocalDateTime;
import com.campushire.entity.InterviewMode;
import jakarta.validation.constraints.NotNull;
import lombok.Getter;
import lombok.Setter;
@Getter
@Setter
public class InterviewRequestDTO {
    @NotNull(message="Application id is required")
    private Long applicationId;
    @NotNull(message="Interview date and time is required")
    private LocalDateTime interviewDateTime;
    @NotNull(message="Interview mode is required")
    private InterviewMode mode;
    private String meetingLink;
}