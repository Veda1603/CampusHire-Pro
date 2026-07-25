package com.campushire.entity;
import jakarta.persistence.*;
import lombok.*;

@Entity
@Table(name="languages")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class Language {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Long id;
    @ManyToOne
    @JoinColumn(name="student_id",nullable=false)
    private Student student;
    private String languageName;
    private String proficiency;
}