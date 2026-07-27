package com.campushire.service;
import java.util.List;
import org.springframework.stereotype.Service;
import com.campushire.dto.NotificationResponse;
import com.campushire.entity.Notification;
import com.campushire.entity.User;
import com.campushire.entity.NotificationType;
import com.campushire.repository.NotificationRepository;
import com.campushire.repository.UserRepository;
@Service
public class NotificationService{
    private final NotificationRepository notificationRepository;
    private final UserRepository userRepository;
    public NotificationService(NotificationRepository notificationRepository,UserRepository userRepository){
        this.notificationRepository=notificationRepository;
        this.userRepository=userRepository;
    }
    public List<NotificationResponse> getMyNotifications(String email){
        User user=userRepository.findByEmail(email)
                .orElseThrow(()->new RuntimeException("User not found"));
        return notificationRepository.findByUserIdOrderByCreatedAtDesc(user.getId())
                .stream()
                .map(this::mapToResponse)
                .toList();
    }
    public Notification createNotification(User user,String message,NotificationType type){
        Notification notification=new Notification();
        notification.setUser(user);
        notification.setMessage(message);
        notification.setType(type);
        notification.setIsRead(false);
        return notificationRepository.save(notification);
    }
    public NotificationResponse markAsRead(Long id){
        Notification notification=notificationRepository.findById(id)
                .orElseThrow(()->new RuntimeException("Notification not found"));
        notification.setIsRead(true);
        return mapToResponse(notificationRepository.save(notification));
    }
    private NotificationResponse mapToResponse(Notification notification){
        NotificationResponse response=new NotificationResponse();
        response.setId(notification.getId());
        response.setMessage(notification.getMessage());
        response.setType(notification.getType());
        response.setIsRead(notification.getIsRead());
        response.setCreatedAt(notification.getCreatedAt());
        return response;
    }
}