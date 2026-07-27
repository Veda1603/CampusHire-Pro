package com.campushire.dto;
import lombok.Getter;
import lombok.Setter;
import com.campushire.entity.NotificationType;
@Getter
@Setter
public class NotificationRequest{
    private Long userId;
    private String message;
    private NotificationType type;
}