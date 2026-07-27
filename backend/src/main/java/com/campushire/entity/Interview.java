package com.campushire.entity;

import java.time.LocalDateTime;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

@Entity
@Getter
@Setter
@Table(name = "interviews")
public class Interview {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    // Which application this interview belongs to
    @ManyToOne
    @JoinColumn(name = "application_id", nullable = false)
    private Application application;

    // Interview date and time
    private LocalDateTime interviewDateTime;

    // GOOGLE_MEET / ZOOM / OFFLINE
    @Enumerated(EnumType.STRING)
    private InterviewMode mode;

    // Generated meeting link
    private String meetingLink;

    // Google Meet event id / Zoom meeting id
    private String meetingId;

    // Interview status
    @Enumerated(EnumType.STRING)
    private InterviewStatus status;
    private LocalDateTime createdAt;
    @PrePersist
    public void onCreate(){
        createdAt = LocalDateTime.now();
        if(status == null){
            status = InterviewStatus.SCHEDULED;
        }
    }

}