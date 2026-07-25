package com.campushire.entity;

import jakarta.persistence.*;
import lombok.*;

@Entity
@Table(name = "projects")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class Project {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    @ManyToOne
    @JoinColumn(name = "student_id", nullable = false)
    private Student student;
    private String title;
    @Column(length = 2000)
    private String description;
    @Column(name = "technologies_used")
    private String technologiesUsed;
    private String githubLink;
    private String liveDemoLink;
    private String projectImageUrl;
}