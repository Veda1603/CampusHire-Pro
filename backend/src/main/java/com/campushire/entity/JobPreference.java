package com.campushire.entity;

import jakarta.persistence.*;
import lombok.*;

@Entity
@Table(name="job_preferences")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class JobPreference {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Long id;
    @OneToOne
    @JoinColumn(name="student_id",nullable=false)
    private Student student;
    private String preferredJobType;
    private String preferredLocation;
    private String preferredIndustry;
    private Double expectedSalary;
    private Boolean openToRelocation;
}