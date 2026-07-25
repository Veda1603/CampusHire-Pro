package com.campushire.entity;

import java.math.BigDecimal;

import jakarta.persistence.*;
import lombok.*;

@Entity
@Table(name = "educations")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class Education {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    @ManyToOne
    @JoinColumn(name = "student_id", nullable = false)
    private Student student;
    private String qualification;
    private String collegeName;
    private String university;
    private String course;
    private String stream;
    private String gradingSystem;
    private BigDecimal score;
    private Integer passoutYear;
    private String marksheetUrl;
}