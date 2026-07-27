package com.campushire.dto;
import lombok.Getter;
import lombok.Setter;
import java.time.LocalDateTime;
import com.campushire.entity.NotificationType;
@Getter
@Setter
public class NotificationResponse{
    private Long id;
    private String message;
    private NotificationType type;
    private Boolean isRead;
    private LocalDateTime createdAt;
}