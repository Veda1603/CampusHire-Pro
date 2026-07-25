package com.campushire.entity;

import java.time.OffsetDateTime;
import jakarta.persistence.*;
import lombok.*;

@Entity
@Table(name="resumes")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class Resume {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Long id;
    @ManyToOne
    @JoinColumn(name="student_id",nullable=false)
    private Student student;
    private String resumeName;
    private String templateName;
    private String pdfUrl;
    private String docxUrl;
    private Boolean isDefault;
    private OffsetDateTime createdAt;
}