package com.campushire.entity;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;
import java.time.LocalDateTime;

@Entity
@Getter
@Setter
@Table(name="notifications")
public class Notification {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Long id;
    @ManyToOne
    @JoinColumn(name="user_id",nullable=false)
    private User user;
    @Column(nullable=false,length=500)
    private String message;
    @Enumerated(EnumType.STRING)
    private NotificationType type;
    private Boolean isRead=false;
    private LocalDateTime createdAt;
    @PrePersist
    public void onCreate(){
        createdAt=LocalDateTime.now();
    }
}