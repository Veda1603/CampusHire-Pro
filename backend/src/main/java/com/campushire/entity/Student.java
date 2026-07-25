package com.campushire.entity;

import java.math.BigDecimal;
import java.util.List;

import jakarta.persistence.*;
import lombok.*;

@Entity
@Table(name = "students")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class Student {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;
    @OneToOne
    @JoinColumn(name = "user_id", nullable = false, unique = true)
    private User user;
    @Column(name = "college_roll_no")
    private String collegeRollNo;
    @Column(name = "prn_number", unique = true)
    private String prnNumber;
    @Column(name = "current_year")
    private Integer currentYear;
    @Column(name = "current_cgpa")
    private BigDecimal currentCGPA;
    @Column(name = "expected_passout_year")
    private Integer expectedPassoutYear;
    @Column(name = "profile_completed")
    @Builder.Default
    private Boolean profileCompleted = false;
    @Enumerated(EnumType.STRING)
    @Column(name = "verification_status")
    @Builder.Default
    private VerificationStatus verificationStatus = VerificationStatus.PENDING;
    @OneToOne(mappedBy = "student")
    private StudentProfile profile;
    @OneToMany(mappedBy = "student", cascade = CascadeType.ALL)
    private List<Education> educations;
    @OneToMany(mappedBy = "student", cascade = CascadeType.ALL)
    private List<Language> languages;    
    @OneToMany(mappedBy = "student", cascade = CascadeType.ALL)
    private List<Certification> certifications;
    @OneToMany(mappedBy = "student", cascade = CascadeType.ALL)
    private List<Project> projects;
    @OneToMany(mappedBy="student",cascade=CascadeType.ALL)
    private List<Document> documents;
    @OneToMany(mappedBy="student",cascade=CascadeType.ALL)
    private List<Resume> resumes;
    @OneToOne(mappedBy="student",cascade=CascadeType.ALL)
    private JobPreference jobPreference;
    @OneToOne(mappedBy="student",cascade=CascadeType.ALL)
    private SocialLink socialLink;
}