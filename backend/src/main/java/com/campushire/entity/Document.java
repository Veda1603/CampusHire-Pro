package com.campushire.entity;

import java.time.OffsetDateTime;
import jakarta.persistence.*;
import lombok.*;

@Entity
@Table(name="documents")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class Document {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Long id;
    @ManyToOne
    @JoinColumn(name="student_id",nullable=false)
    private Student student;
    @Column(name="document_type")
    private String documentType;
    private String documentName;
    private String fileUrl;
    private OffsetDateTime uploadedAt;
}