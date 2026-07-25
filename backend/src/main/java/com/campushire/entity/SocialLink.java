package com.campushire.entity;

import jakarta.persistence.*;
import lombok.*;

@Entity
@Table(name="social_links")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class SocialLink {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Long id;
    @OneToOne
    @JoinColumn(name="student_id",nullable=false)
    private Student student;
    private String linkedin;
    private String github;
    private String portfolio;
    private String leetcode;
    private String hackerrank;
}