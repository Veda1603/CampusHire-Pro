package com.campushire.controller;
import java.util.List;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.NotificationResponse;
import com.campushire.service.NotificationService;
import lombok.RequiredArgsConstructor;
@RestController
@RequestMapping("/api/notifications")
@RequiredArgsConstructor
public class NotificationController{
    private final NotificationService notificationService;

    @PreAuthorize("hasAnyRole('STUDENT','RECRUITER')")
    @GetMapping
    public ResponseEntity<List<NotificationResponse>> getNotifications(Authentication authentication){
        return ResponseEntity.ok(notificationService.getMyNotifications(authentication.getName()));
    }

    @PreAuthorize("hasAnyRole('STUDENT','RECRUITER')")
    @PutMapping("/{id}/read")
    public ResponseEntity<NotificationResponse> markRead(@PathVariable Long id){
        return ResponseEntity.ok(notificationService.markAsRead(id));
    }
}